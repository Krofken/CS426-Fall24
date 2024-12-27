using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour, IInteractable
{
    private bool isHeld = false; // Tracks whether the cube is being held
    public Transform playerHoldPoint; // The point where the cube will be held by the player
    private Rigidbody rb; // Rigidbody of the cube

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        if (!isHeld)
        {
            // Pick up the cube
            isHeld = true;
            rb.isKinematic = true; // Disable physics while held
            rb.useGravity = false;
            transform.SetParent(playerHoldPoint);
            transform.localPosition = Vector3.zero; // Position cube at the hold point
        }
        else
        {
            // Drop the cube
            isHeld = false;
            rb.isKinematic = false; // Enable physics
            rb.useGravity = true;
            transform.SetParent(null); // Detach from player
        }
    }

    private void FixedUpdate()
    {
        if (isHeld)
        {
            // Ensure the cube stays at the player's hold point
            transform.position = playerHoldPoint.position;
        }
    }
}
