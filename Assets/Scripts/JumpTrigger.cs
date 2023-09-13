using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public AudioSource scream;
    //public GameObject theEnemy;
    public GameObject thePlayer;
    public GameObject jumpcam;
    private JumpScare jumpScarex;

    private void Awake()
    {
        jumpScarex = FindAnyObjectByType<JumpScare>();
        
    }
    private void Start()
    {
        //jumpScare.jumpScare = true;
    }
    private void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        scream.Play();
        jumpcam.SetActive(true);
        thePlayer.SetActive(false);
        jumpScarex.jumpScaree = false;
    }

}
