using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipStick : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject light;
    [SerializeField] private GameObject lipSticks;
    //public GameObject flash;
    


    void Start()
    {

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement = GetComponent<PlayerMovement>();
       //flash = GameObject.FindGameObjectWithTag("Flash");
        //flash.SetActive(false);

    }

    // Update is called once per frame
    private void Update()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Destroy(gameObject, 0.3f);
            PlayerMovement health = FindObjectOfType<PlayerMovement>();
            health.maxHealth++;
            



        }
    }




}


