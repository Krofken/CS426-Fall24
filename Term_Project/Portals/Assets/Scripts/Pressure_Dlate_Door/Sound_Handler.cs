using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Handler : MonoBehaviour

{
    public AudioSource audioSource; // Assign via Inspector
    public AudioClip soundEffect;   // Assign via Inspector

    // Method to play the sound
    public void PlaySoundEffect(AudioClip clip)
    {
        if (audioSource && clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
