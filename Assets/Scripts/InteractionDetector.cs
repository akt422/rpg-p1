using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Dialogue;
using Interactions;
using TMPro;
using UnityEngine.SceneManagement;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> interactablesInRange = new List<IInteractable>();
    [SerializeField] private GameObject interactIcon;
    [SerializeField] private GameObject interactImage;
    [SerializeField] private TMP_Text interactText;
    [SerializeField] private InteractionPromptUI interactionPromptUI; // use this to separate concerns of showing stuff on panel

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This is so that interactables list is cleared when new scene is loaded. we dont want the list carried over to other scenes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        interactablesInRange.Clear();
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.TryGetComponent(out IInteractable interactable))
        {
            if (!interactablesInRange.Contains(interactable))
            {
                interactablesInRange.Add(interactable);
            }
        }
    }

    public void Update()
    {
        if (GameStateManager.GetState() != GameState.Gameplay)
        {
            interactIcon.SetActive(false);
            interactImage.SetActive(false);
            return;
        }
        bool closestFound = FindClosest() != null;
        interactIcon.SetActive(closestFound);
        interactImage.SetActive(closestFound);
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        // if (obj.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        // {
        //     interObj = obj;
        // }
        if (obj.TryGetComponent(out IInteractable interactable) && interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Remove(interactable);
        }
    }

    private IInteractable FindClosest()
    {
        if (interactablesInRange.Count == 0) return null;
        float currDist = Mathf.Infinity;
        IInteractable closest = null;
        foreach (IInteractable interactable in interactablesInRange)
        {
            if (!interactable.CanInteract())
            {
                continue;
            }

            MonoBehaviour interactableBehaviour = interactable as MonoBehaviour;

            if (!interactableBehaviour)
            {
                continue;
            }

            float distance = Vector2.Distance(
                transform.position,
                interactableBehaviour.transform.position
            );

            if (distance < currDist)
            {
                currDist = distance;
                closest = interactable;
            }
        }
        interactText.text = closest?.InteractText();
        return closest;
    }

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed) return;
        if (GameStateManager.GetState() == GameState.Dialogue)
        {
            DialogueManager.Instance.AdvanceDialogue();
            return;
        }

        if (GameStateManager.GetState() != GameState.Gameplay)
        {
            return;
        }
        IInteractable closest = FindClosest();
        closest?.Interact();
    }
}
