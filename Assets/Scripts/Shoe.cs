using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : MonoBehaviour
{
    public bool isThrowed;
    public Vector3 rotateAxis;
    public bool isPlayerShoe;

    private void Update()
    {
        if (isThrowed)
        {
            transform.Rotate(rotateAxis * Time.deltaTime);
        }
    }
}
