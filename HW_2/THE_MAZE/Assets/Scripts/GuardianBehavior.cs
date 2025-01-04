using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianBehavior : MonoBehaviour {
    public Transform pointA; // First patrol point
    public Transform pointB; // Second patrol point
    public float patrolSpeed = 3f; // Speed while patrolling
    public float chaseSpeed = 6f; // Speed while chasing
    public float detectionRange = 5f; // Distance to detect the player
    public Transform player; // Reference to the player

    private bool isChasing = false; // Is the guardian chasing the player?
    private Transform currentTarget; // Current patrol target

    void Start() {
        // Start patrolling initially
        currentTarget = pointB;
        StartCoroutine(Patrol());
    }

    void Update() {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange) {
            if (!isChasing) {
                isChasing = true; // Start chasing
                StopCoroutine(Patrol()); // Stop patrolling
            }
            ChasePlayer(); // Chase the player
        }
        else if (isChasing) {
            // Stop chasing and resume patrolling if the player is out of range
            isChasing = false;
            StartCoroutine(Patrol());
        }
    }

    IEnumerator Patrol() {
        while (!isChasing) // Patrol only when not chasing
        {
            // Move towards the current patrol target
            while (Vector3.Distance(transform.position, currentTarget.position) > 0.01f) {
                MoveTowards(currentTarget.position, patrolSpeed);
                yield return null;
            }

            // Snap to the target position
            transform.position = currentTarget.position;

            // Switch to the other patrol point
            currentTarget = currentTarget == pointA ? pointB : pointA;

            // Wait briefly before moving again
            yield return new WaitForSeconds(1f);
        }
    }

    void ChasePlayer() {
        // Move towards the player's position
        MoveTowards(player.position, chaseSpeed);
    }

    void MoveTowards(Vector3 targetPosition, float speed) {
        // Calculate the direction to the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move the guardian
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate the guardian to face the direction of movement
        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
        }
    }

    void OnTriggerEnter(Collider other) {
        // Check if the guardian catches the player
        if (other.CompareTag("Player")) {
            Debug.Log("Player caught by the guardian!");
            // Trigger game over
            FindObjectOfType<GameOverGUI>().TriggerGameOver();
            Destroy(other.gameObject); // Replace with death or respawn logic
        }
    }

    void OnDrawGizmosSelected() {
        // Visualize the detection range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
