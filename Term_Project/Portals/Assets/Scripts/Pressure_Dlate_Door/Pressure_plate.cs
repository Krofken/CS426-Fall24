using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_plate : MonoBehaviour
{
    private Animator animator;
    private int objectsOnPlate = 0; // Tracks how many objects are on the plate

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cube"))
        {
            if (objectsOnPlate == 0) // Open the door only if it was closed
            {
                animator.SetTrigger("Open");
            }
            objectsOnPlate++; // Increment the counter
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cube"))
        {
            objectsOnPlate--; // Decrement the counter
            if (objectsOnPlate <= 0) // Close the door only if no objects remain
            {
                animator.SetTrigger("Closed");
                objectsOnPlate = 0; // Ensure the counter doesn't go negative
            }
        }
    }
}