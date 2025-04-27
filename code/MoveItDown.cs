using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItDown : MonoBehaviour
{
    public string tag;
    public Vector3 moveDistance = new Vector3(0, -0.008f, 0); // Distance to move the object down
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool shouldMove = false;
    public float smoothTime = 0.2f; // Time to smooth the movement
    private Vector3 velocity = Vector3.zero;
    private AudioSource audioSource;
    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            targetPosition = originalPosition + moveDistance;
            shouldMove = true;
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tag))
        {
            targetPosition = originalPosition;
            shouldMove = true;
        }
    }
}