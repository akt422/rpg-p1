using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    private float fadeOutDuration = 0.3f;
    private float fadeInDuration = 0.3f;
    private float fadeDuration = 0.5f;
    [SerializeField] private GameObject fadeOverlay;
    private CanvasGroup fadeOverlayImage;
        
    private bool isLoading;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        fadeOverlayImage = fadeOverlay.GetComponent<CanvasGroup>();
    }
    
    public void LoadScene(string sceneName)
    {
        if (isLoading)
            return;
        Debug.Log(sceneName + " is loading...");
        StartCoroutine(LoadAsync(sceneName));
        // StartCoroutine(SceneTransition(sceneName)); // Experimenting with Light objects dimming
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        isLoading = true;
        fadeOverlayImage.blocksRaycasts = true; // set true to prevent player from opening menu or something while fading in/out
        
        yield return StartCoroutine(FadeTransition(0f, 1f));
        
        AsyncOperation operation =
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!operation.isDone)
        {
            yield return null;
        }
        
        yield return StartCoroutine(FadeTransition(1f, 0f));

        fadeOverlayImage.blocksRaycasts = false; // set true above to prevent player from opening menu or something while fading in/out
        isLoading = false;
    }

    private IEnumerator FadeTransition(float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeOverlayImage.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }
        fadeOverlayImage.alpha = end;
    }

    private IEnumerator SceneTransition(string sceneName)
    {
        isLoading = true;

        yield return StartCoroutine(FadeOutCurrScene());

        AsyncOperation operation =
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!operation.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeInNewScene());
        
        isLoading = false;
    }

    private IEnumerator FadeOutCurrScene()
    {
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Light");

        List<Light2D> lights = new();
        List<float> startIntensities = new();

        foreach (GameObject lightObj in lightObjects)
        {
            Light2D light = lightObj.GetComponent<Light2D>();
            if (light != null)
            {
                lights.Add(light);
                startIntensities.Add(light.intensity);
            }
        }
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / fadeOutDuration;

            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].intensity =
                    Mathf.Lerp(startIntensities[i], 0f, t);
            }

            yield return null;
        }

        foreach (Light2D light in lights)
        {
            light.intensity = 0f;
        }
    }
    
    // IEnumerator FadeInNewScene()
    // {
    //     foreach (GameObject lightObj in GameObject.FindGameObjectsWithTag("Light"))
    //     {
    //         Light2D lightComponent = lightObj.GetComponent<Light2D>();
    //         float startIntensity = 0f;
    //         float endIntensity = lightComponent.intensity;
    //         yield return StartCoroutine(FadeTransition(lightComponent, startIntensity, endIntensity));
    //     }
    // }
    
    private IEnumerator FadeInNewScene()
    {
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Light");

        List<Light2D> lights = new();
        List<float> targetIntensities = new();

        foreach (GameObject lightObj in lightObjects)
        {
            Debug.Log(lightObj.name);
            Light2D light = lightObj.GetComponent<Light2D>();

            if (light != null)
            {
                lights.Add(light);
                targetIntensities.Add(light.intensity);
                light.intensity = 0f;
            }
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / fadeInDuration;

            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].intensity =
                    Mathf.Lerp(0f, targetIntensities[i], t);
            }

            yield return null;
        }

        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = targetIntensities[i];
        }
    }
    
}