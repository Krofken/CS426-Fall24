using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverGUI : MonoBehaviour {
    private bool isGameOver = false; // Tracks whether the game is over
    public Camera secondaryCamera;

    void OnGUI() {
        if (isGameOver) {
            // Enable cursor visibility and unlock it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Center the buttons on the screen
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 24;

            if (GUI.Button(new Rect(screenWidth / 2 - 100, screenHeight / 2 - 50, 200, 50), "Restart", buttonStyle)) {
                RestartGame();
            }

            if (GUI.Button(new Rect(screenWidth / 2 - 100, screenHeight / 2 + 10, 200, 50), "Exit", buttonStyle)) {
                ExitGame();
            }
        }
    }

    public void TriggerGameOver() {
        isGameOver = true;

        // Enable cursor for GUI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Enable the secondary camera
        if (secondaryCamera != null) {
            secondaryCamera.gameObject.SetActive(true);
        }
    }

    void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    void ExitGame() {
        Debug.Log("Exiting Game...");
        Application.Quit(); // Quit the game (works in a built application, not in the editor)
    }
}
