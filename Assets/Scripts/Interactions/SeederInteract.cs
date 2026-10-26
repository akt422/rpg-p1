using Dialogue;
using Interactions;
using UnityEngine;

public class SeederInteract : MonoBehaviour, IInteractable
{
    private string interactText = "Press E - Buy seed";
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private ItemData[] seeds;

    void OnEnable()
    {
        DialogueManager.Instance.dialogueActionTriggered += OpenMenu;
    }

    void OnDisable()
    {
        DialogueManager.Instance.dialogueActionTriggered -= OpenMenu;
    }
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueData);
    }

    public string InteractText()
    {
        return interactText;
    }

    private void OpenMenu(string eventId)
    {
        ShopController.Instance.InstantiateItems(seeds);
    }
}