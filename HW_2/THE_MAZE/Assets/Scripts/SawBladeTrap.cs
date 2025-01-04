using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeTrap : MonoBehaviour {
    public Transform pointA; // The first point
    public Transform pointB; // The second point
    public float speed = 5f; // Speed of the sawblade movement
    public float waitTime = 1f; // Time to wait at each point

    private Transform currentTarget; // Current target point
    private bool isMoving = true; // Is the sawblade currently moving?

    void Start() {
        // Start the coroutine to handle periodic movement
        currentTarget = pointB;
        StartCoroutine(MoveSawblade());
    }

    IEnumerator MoveSawblade() {
        while (true) {
            // Move towards the current target
            while (Vector3.Distance(transform.position, currentTarget.position) > 0.1f) {
                if (isMoving) {
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
                }
                yield return null;
            }

            // Switch target point
            currentTarget = currentTarget == pointA ? pointB : pointA;

            // Wait before moving again
            yield return new WaitForSeconds(waitTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        // Check if the sawblade hits the player
        if (other.CompareTag("Player")) {
            // Replace this with your logic for player death
            Debug.Log("Player Died");
            // Trigger game over
            FindObjectOfType<GameOverGUI>().TriggerGameOver();
            Destroy(other.gameObject); // Destroy the player for now (can be replaced with a death event)
        }
    }
}
