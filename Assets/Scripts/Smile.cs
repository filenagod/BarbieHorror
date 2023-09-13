using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smile : MonoBehaviour
{
    public AudioSource smilee;
    public GameObject thePlayer;
    private void Start()
    {
        smilee = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter()
    {
        smilee.Play();
    }
}
