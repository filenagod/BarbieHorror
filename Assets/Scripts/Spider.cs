using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private JumpScare jumpScare;
    private AudioSource audioSource;

    private void Awake()
    {
        jumpScare = FindAnyObjectByType<JumpScare>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        jumpScare.jumpScaree = true;
    }

    private void Update()
    {
        Invoke(nameof(DestroyObject), 3f);    
    }

    private void DestroyObject()
    {
        jumpScare.jumpScaree = false;
        Destroy(gameObject);
    }
}
