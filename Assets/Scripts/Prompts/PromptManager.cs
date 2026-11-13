
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PromptManager : MonoBehaviour
{
    public static PromptManager Instance { get; private set; }
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private TMP_Text promptText;
    private bool promptActive;
    private bool transitioning;
    private Coroutine promptRoutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        promptPanel.SetActive(false);
    }

    public void StartPrompt(string prompt)
    {
        if (promptActive || transitioning)
            return;

        promptPanel.SetActive(true);
        promptText.enabled = true;
        promptText.alpha = 1f;
        promptText.text = prompt;

        promptActive = true;
        promptRoutine = StartCoroutine(PromptCoroutine());
    }

    private void Update()
    {
        if (!promptActive || transitioning ||
            Keyboard.current == null)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            transitioning = true;
            ClosePrompt();
            
            SceneTransitionManager.Instance.LoadScene("start-room");
            GameStateManager.SetState(GameState.Gameplay);
        }
    }

    private void ClosePrompt()
    {
        promptActive = false;

        if (promptRoutine != null)
        {
            StopCoroutine(promptRoutine);
            promptRoutine = null;
        }

        promptPanel.SetActive(false);
        promptText.alpha = 1f;
        promptText.text = "";
    }
    
    IEnumerator PromptCoroutine()
    {
        while (true)
        {
            promptText.alpha = 1f;
            yield return new WaitForSeconds(1f);
        
            promptText.alpha = 0f;
            yield return new WaitForSeconds(1f); 
        }
    }
}