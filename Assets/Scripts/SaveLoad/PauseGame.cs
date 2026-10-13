using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// todo: Write fade-out for pause menu, also incorporate fading between menu tabs
public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button selectedButton;
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    [SerializeField] private float fadeDuration = 0.3f;

    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameStateManager.GetState() == GameState.Pause)
            {
                Resume();
            }
            else if (GameStateManager.GetState() == GameState.Gameplay)
            {
                Pause();
                EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);

            }
        }
    }

    public void Pause()
    {
        GameStateManager.SetState(GameState.Pause);
        pausePanel.SetActive(true);
        
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        
        StartCoroutine(FadeIn());
        
        Time.timeScale = 0f;
    }
    private IEnumerator FadeIn()
    {
        yield return Fade(1f);

        pauseCanvasGroup.interactable = true;
        pauseCanvasGroup.blocksRaycasts = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
    }
    public void Resume()
    {
        GameStateManager.SetState(GameState.Gameplay);
        EventSystem.current.SetSelectedGameObject(null);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void Quit()
    {
        Debug.Log("Goodbye");
        Application.Quit();
    }

    private IEnumerator Fade(float endAlpha)
    {
        float startAlpha = pauseCanvasGroup.alpha;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // Cannot use Delta time bc it's paused and Delta time wont change
            pauseCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }
        pauseCanvasGroup.alpha = endAlpha;
    }
}