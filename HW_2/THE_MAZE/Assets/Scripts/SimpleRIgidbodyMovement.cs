using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRigidbodyMovement : MonoBehaviour {
    public float walkSpeed = 5f;    // Walking speed
    public float runSpeed = 10f;   // Running speed
    public Transform cameraTransform; // Reference to the camera's Transform

    private Rigidbody rb;          // Reference to the Rigidbody component

    void Start() {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody is set up correctly
        rb.freezeRotation = true; // Prevent unwanted rotation due to physics
    }

    void FixedUpdate() {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float speed = isRunning ? runSpeed : walkSpeed;

        // Use the camera's forward and right directions for movement
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten the forward and right vectors
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Calculate the desired movement direction
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized * speed;

        // Apply the movement to the Rigidbody
        Vector3 newPosition = rb.position + moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
