using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float trunSepped = 15f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float throwSpeed = 10f;
    public List<Transform> turtle;
    [SerializeField] private CameraController cam;
    [SerializeField] private float turtleDistance;
    [SerializeField] private GameObject shoe;
    public int shoesCount;
    [SerializeField] private int maxShoesCount = 5;
    [SerializeField] private GameObject openPanel;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private TextMeshProUGUI shoeCountText;
    [SerializeField] private TextMeshProUGUI healthCountText;
    public int maxHealth;
    public int currentHealth; // Mevcut can miktarý
    //[SerializeField] private Text chestt;
    public LayerMask stairsLayer;
    public LayerMask turtleLayer;
    public LayerMask interaction;
    public float raycastDistance = 1f;
    public float stairsSpeed = 5f;
    private bool isTouchingStairs = false;
    private bool jumping = false;
    private bool throwing = false;
    public float interactionDistance = 5f; // Oyuncunun kapýyla etkileþim mesafesi
    private ChestController chest; // Kapý script'ine eriþim için deðiþken
    public bool chestOpened;
    private GameObject balta;

    [SerializeField] private AudioClip clipToPlay;
    private AudioSource audioSource;




    private float verticalVelocity;

    private bool moveLeft;
    private bool moveRight;

    public int GetShoesCount
    {
        get { return shoesCount; }
        set
        {
            shoesCount = value;
            if (shoesCount > maxShoesCount) shoesCount = maxShoesCount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        chest = FindObjectOfType<ChestController>();
        balta = GameObject.FindGameObjectWithTag("Balta");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipToPlay;

        //turtleMovement = turtle.GetComponent<Turtle>();
    }

    // Update is called once per frameh
    void Update()
    {
        shoeCountText.text = shoesCount.ToString();
        healthCountText.text = maxHealth.ToString();

        if (maxHealth <= 0)
        {
            gameObject.SetActive(false); // Can sýfýr veya daha az ise nesneyi yok et
        }

        if (shoesCount > 0)
        {
            shoe.SetActive(true);
        }
        else
        {
            shoe.SetActive(false);
        }
        

        TakeInput();
        InteractWithTurtle();
        ChestOpening();

        if(turtle.Count > 0)
        {
            if (CheckDistanceFromTurtle() < turtleDistance)
            {
                cam.xOffset = -5f;
            }
            else
            {
                cam.xOffset = -3.5f;
            }
        }
        else
        {
            cam.xOffset = -3.5f;
        }

    }

    // Fizik iþlemlerini burda yap (Rigidbody)

    private void FixedUpdate()
    {
        if (moveLeft)
        {
            // rigidbody.velocity.y olmasýnýn sebebi hareket ederken ayný zamanda zýplayabilmek için
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, -speed * Time.fixedDeltaTime);
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 180f, 0f), trunSepped * Time.deltaTime);
        }

        else if (moveRight)
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, speed * Time.fixedDeltaTime);
            //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), trunSepped * Time.deltaTime);
        }

        else if (!moveLeft && !moveRight)
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
        }

        if (jumping)
        {
            rigidbody.AddForce(new Vector3(rigidbody.velocity.x, jumpPower * Time.fixedDeltaTime, 0f));
            jumping = false;
        }

        // Set the starting point and direction for the Raycast
        // If your character's upward direction aligns with stairs, this should be enough; otherwise, adjust it accordingly.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, stairsLayer))
        {
            // If the character is touching stairs
            isTouchingStairs = true;
        }
        else
        {
            // If the character is not touching stairs
            isTouchingStairs = false;
        }

        // If touching stairs, move the character upwards
        if (isTouchingStairs)
        {
            float moveY = Input.GetAxis("Horizontal") * stairsSpeed;
            transform.Translate(new Vector3(0f, moveY, 0f) * Time.deltaTime);
        }

       

    }
    private void TakeInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
            moveRight = false;
            animator.SetBool("IsWalking", true);
            animator.SetBool("Ascending", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
            moveLeft = false;
            animator.SetBool("IsWalking", true);
            animator.SetBool("Ascending", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
        {
            jumping = true;
            animator.SetTrigger("Jumping");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (shoesCount > 0)
            {
                throwing = true;
                animator.SetTrigger("Throwing");
            }
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            moveRight = false;
            moveLeft = false;
            animator.SetBool("IsWalking", false);
            animator.SetBool("Ascending", false);
        }





    }

    private void InteractWithTurtle()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.5f, turtleLayer))
        {
            if (hit.collider.GetComponent<Turtle>() != null)
            {
               
                hit.collider.GetComponent<Turtle>().moveTurtle = true;
                transform.SetParent(hit.collider.transform);

            }
        }
    }

    private bool OnGroundCheck()
    {
        bool hit = Physics.Raycast(transform.position, Vector3.down, 0.25f);

        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    private float CheckDistanceFromTurtle()
    {
        
         return Vector3.Distance(transform.position, turtle[0].position);
        
        
    }

    private void ChestOpening()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.blue);
            if (hit.collider.gameObject.CompareTag("Chest") && !hit.collider.GetComponent<ChestController>().isChestOpened)
            {
                openPanel.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<ChestController>().OpenChest();
                }
            }

            else
            {
                openPanel.gameObject.SetActive(false);
            }

        }
    }


    public void ThrowShoes()
    {
        if (shoesCount > 0)
        {
            shoesCount--;
        }

        GameObject shoesClone = Instantiate(shoe, fireTransform.position, transform.rotation);
        shoesClone.GetComponent<Shoe>().isPlayerShoe = false;
        shoesClone.transform.SetParent(null);
        shoesClone.GetComponent<Rigidbody>().isKinematic = false;
        shoesClone.GetComponent<Rigidbody>().AddForce(transform.forward * 300f);
        shoesClone.GetComponent<Collider>().isTrigger = false;
        shoesClone.GetComponent<Shoe>().isThrowed = true;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Balta")) // Eðer çarpýþan nesne "Enemy" etiketine sahipse
        {
            maxHealth--;
            if (!audioSource.isPlaying) // Eðer ses zaten çalmýyorsa
            {
                audioSource.Play(); // Sesi çal
            }
        }
    }
}