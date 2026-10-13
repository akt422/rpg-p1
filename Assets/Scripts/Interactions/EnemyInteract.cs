using System;
using Dialogue;
using Interactions;
using UnityEngine;

public class EnemyInteract : MonoBehaviour, IInteractable
{
    private string interactText = "E - Fight";
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnPoint;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite activeSprite;
    private string eventId = "combat_boss_1";
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
    }
    
    private void OnEnable()
    {
        DialogueManager.Instance.dialogueActionTriggered += HandleDialogueAction;
    }

    private void OnDisable()
    {
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.dialogueActionTriggered -= HandleDialogueAction;
    }
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        sr.sprite = activeSprite;
        DialogueManager.Instance.StartDialogue(dialogueData);
    }

    public string InteractText()
    {
        return interactText;
    }

    private void HandleDialogueAction(string eventId)
    {
        if (eventId == this.eventId)
        {
            SceneTransitionData.targetSpawnPoint = targetSpawnPoint;
            SceneTransitionManager.Instance.LoadScene(targetScene);
        }
    }
}