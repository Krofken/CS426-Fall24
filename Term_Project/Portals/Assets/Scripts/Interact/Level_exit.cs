using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_exit : MonoBehaviour, IInteractable 
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float delay = 1f;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioClip interactionSound; // Sound to play on interaction
    [SerializeField] private AudioSource audioSource;


    public void Interact()
    {
        audioSource.PlayOneShot(interactionSound);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;  // Increment elapsed time by frame time
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // Lerp alpha
            yield return null;  // Wait for the next frame
        }

        canvasGroup.alpha = 1;  // Ensure fully visible at the end
        yield return new WaitForSeconds(delay);  // Wait for 1 second
        SceneManager.LoadScene(sceneToLoad);
    }
}
