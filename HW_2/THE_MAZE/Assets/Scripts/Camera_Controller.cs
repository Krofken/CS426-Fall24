using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_Controller : MonoBehaviour {
    public Transform playerBody;       // Reference to the player's body
    public float mouseSensitivity = 100f; // Sensitivity for mouse movement
    public float cameraPitchLimit = 90f;  // Limit for vertical camera rotation

    private float xRotation = 0f;      // To track vertical rotation

    void Start() {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -cameraPitchLimit, cameraPitchLimit); // Limit vertical rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
