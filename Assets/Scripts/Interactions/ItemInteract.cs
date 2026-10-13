using Dialogue;
using Interactions;
using UnityEngine;

public abstract class ItemInteract : MonoBehaviour, IInteractable
{
    private bool collected = false;
    [SerializeField] private string pickUpText;
    [SerializeField] private AudioClip keyPickClip;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private ItemData item;
    
    public bool CanInteract()
    {
        return !collected;
    }

    public void Interact()
    {
        if(collected) return;
        collected = true;
        Bag.AddItem(item, 1);
        AudioManager.Instance.PlaySound(keyPickClip);
        DialogueManager.Instance.StartDialogue(dialogueData);
        gameObject.SetActive(false);
    }

    public abstract string InteractText();
}