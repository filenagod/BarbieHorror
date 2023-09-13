using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class HorrorGirl : MonoBehaviour
{
    [SerializeField] public float speed = 50f;
    [SerializeField] public float stopDistance = 1f;
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private AudioClip clipToPlay;
    private AudioSource audioSource;
    private int currentHealth; // Mevcut can miktarý
    public Animator animator;
    Rigidbody rb;
    public LayerMask playerLayer;
    public Transform rayTransform;
    private Transform target;
    private GameObject shoes;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        shoes = GameObject.FindGameObjectWithTag("Shoes");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipToPlay;
       
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }
    private void Aim()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayTransform.position, transform.forward, out hit, 5f, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!audioSource.isPlaying) // Eðer ses zaten çalmýyorsa
                {
                    audioSource.Play(); // Sesi çal
                }
                if (target != null)
                {
                    transform.LookAt(target);
                }
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance > stopDistance)
                {
                    transform.position += transform.forward * speed * Time.deltaTime;
                    animator.SetTrigger("HorrorRun");
                }
                else if (distance <= stopDistance)
                {
                    animator.SetTrigger("Attack");
                }

                
            }
        }
        Debug.DrawRay(rayTransform.position, transform.forward * 5f, Color.blue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shoes")) // Eðer çarpýþan nesne "Enemy" etiketine sahipse
        {
            maxHealth--; // Caný azalt
            if (maxHealth <= 0)
            {
                Destroy(gameObject); // Can sýfýr veya daha az ise nesneyi yok et
            }

            Destroy(collision.gameObject);


        }



    }

}
