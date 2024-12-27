using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float delay = 1f;
    [SerializeField] private bool isFade_in = true;



    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvasGroup.alpha = 0;  // Start fully transparent
        if (isFade_in)
        {
            canvasGroup.alpha = 0;
            StartCoroutine(FadeIn());
        }
        else
        {
            canvasGroup.alpha = 1;
            StartCoroutine(FadeOut());
        }  // Start the fade-in coroutine

    }
    void Update()
    {   
                // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
             StartCoroutine(FadeInPlay(SceneManager.GetActiveScene().name));
            // Reload the current scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(FadeInQuit());           
        }

    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(delay);  // Wait for 1 second
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;  // Increment elapsed time by frame time
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // Lerp alpha
            yield return null;  // Wait for the next frame
        }

        canvasGroup.alpha = 1;  // Ensure fully visible at the end
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }

        canvasGroup.alpha = 0;  // Ensure fully transparent at the end
    }

    private IEnumerator FadeOutPlay(string sceneToload)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }

        canvasGroup.alpha = 0;  // Ensure fully transparent at the end
        yield return new WaitForSeconds(delay);  // Wait for 1 second
        SceneManager.LoadScene(sceneToload);
    }

    private IEnumerator FadeInPlay(string sceneToload)
    {
        yield return new WaitForSeconds(delay);  // Wait for 1 second
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;  // Increment elapsed time by frame time
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // Lerp alpha
            yield return null;  // Wait for the next frame
        }

        canvasGroup.alpha = 1;  // Ensure fully visible at the end
        SceneManager.LoadScene(sceneToload);
    }

        private IEnumerator FadeInQuit()
    {
        yield return new WaitForSeconds(delay);  // Wait for 1 second
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;  // Increment elapsed time by frame time
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // Lerp alpha
            yield return null;  // Wait for the next frame
        }

        canvasGroup.alpha = 1;  // Ensure fully visible at the end
        Application.Quit();
    }


    private IEnumerator FadeOutQuit()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }

        canvasGroup.alpha = 0;  // Ensure fully transparent at the end
        yield return new WaitForSeconds(delay);  // Wait for 1 second
        

    }


    public void Play()
    {
        StartCoroutine(FadeOutPlay("Level_1"));  // Start the fade-in coroutine
    }

    public void Quit()
    {
        StartCoroutine(FadeOutQuit());  // Start the fade-in coroutine
    }

}
