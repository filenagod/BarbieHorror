using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private List<GameObject> mirorParts = new List<GameObject>();
    private AudioSource audioSource;

    private bool isMirrorCracked;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Shoes"))
        {
            if (!isMirrorCracked && !other.gameObject.GetComponent<Shoe>().isPlayerShoe)
            {
                audioSource.Play();
                foreach (GameObject part in mirorParts)
                {
                    part.GetComponent<Rigidbody>().isKinematic = false;
                }
                Destroy(other.gameObject);
                isMirrorCracked = true;
            }
        }
    }
}

