using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_of_Audio : MonoBehaviour {
    private AudioSource audioSource;
    private bool isPaused = false;

    void Start() {

        audioSource = GetComponent<AudioSource>();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.M)) {

            if (audioSource.isPlaying && !isPaused) {
                audioSource.Pause();
                isPaused = true;
            }

            else if (isPaused) {
                audioSource.UnPause();
                isPaused = false;
            }
        }
    }
}