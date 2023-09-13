using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Transform spawnPos;
    private AudioSource audioSource;
    [SerializeField] private AudioClip clipToPlay;
    
    public bool isLipstickChest;
    public bool isSpiderChest;

    [SerializeField] private GameObject spider;
    [SerializeField] private GameObject lipstick;

    public GameObject currentChestObject;
    public bool isChestOpened;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipToPlay;
        
    }


    public void OpenChest()
    {
        isChestOpened = true;
        animator.SetBool("IsOpen", true);
        InstantiateObject();
        audioSource.Play();
    }
       

    private void InstantiateObject()
    {
        if (isSpiderChest)
        {
            currentChestObject = Instantiate(spider, spawnPos.position, Quaternion.identity);
        }

        if (isLipstickChest)
        {
            currentChestObject = Instantiate(lipstick, spawnPos.position, Quaternion.identity);
        }
    }
}
