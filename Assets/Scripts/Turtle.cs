using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] private Transform finalPos;
    [SerializeField] private float lerpSpeed;
    public bool moveTurtle;
    [SerializeField] private Transform player;
    PlayerMovement turtlesx;
    public bool turtleRemove = false;
    [SerializeField] private AudioClip clipToPlay;
    private AudioSource audioSource;


    private void Start()
    {
        turtlesx = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipToPlay;
    }
    private void Update()
    {
        if (moveTurtle)
        {
            GoPos();
        }

        if (transform.position == finalPos.position)
        {
            if(!turtleRemove && turtlesx.turtle.Count > 0)
            {
                player.SetParent(null);
                turtleRemove = true;
                turtlesx.turtle.Remove(turtlesx.turtle[0]);
                if (!audioSource.isPlaying) // Eðer ses zaten çalmýyorsa
                {
                    audioSource.Play(); // Sesi çal
                }

            }
        }
        
    }

    public void GoPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos.position, lerpSpeed * Time.deltaTime);
        
       
    }
}
