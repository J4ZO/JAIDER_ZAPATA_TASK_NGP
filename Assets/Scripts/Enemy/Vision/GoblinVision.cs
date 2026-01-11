using System;
using UnityEngine;


public class GoblinVision : MonoBehaviour
{
    public bool detectedPlayer = false;
    public Transform DetectedPlayer { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detectedPlayer = true;
            DetectedPlayer = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detectedPlayer = false;
        }
    }
}

