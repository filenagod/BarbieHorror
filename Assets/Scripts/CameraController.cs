using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Vector3 offset;
    public float xOffset = 3.5f;

    private void Update()
    {
        offset.x = xOffset;
        transform.position = Vector3.Lerp(transform.position, target.position - offset, lerpSpeed * Time.deltaTime);
    }
}
