using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    public string keyTag = "Key"; // Tag assigned to the key

    void OnCollisionEnter(Collision collision) {
        // Debug log to check what is colliding
        Debug.Log($"Collided with: {collision.gameObject.name}");

        // Check if the colliding object is the key
        if (collision.gameObject.CompareTag(keyTag)) {
            // Trigger game over
            FindObjectOfType<GameOverGUI>().TriggerGameOver();
            Debug.Log("Game Finished!");
        }
    }
}
