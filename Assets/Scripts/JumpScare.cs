using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    public bool jumpScaree;


    private void Update()
    {
        if (jumpScaree)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);

        }
    }
}
