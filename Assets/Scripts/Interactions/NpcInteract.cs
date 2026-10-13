using Dialogue;
using UnityEngine;

namespace Interactions
{
    public class NpcInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueData dialogueData;
        [SerializeField] private DialogueData notEnoughCostDialogue;
        [SerializeField] private ItemData priceItem;
        [SerializeField] private string eventId;
        private string interactText = "E - Talk";
    
        public bool CanInteract()
        {
            return true;
        }

        public void Interact()
        {
            if (StoryStateManager.HasFlag(eventId))
            {
                DialogueManager.Instance.StartDialogue(dialogueData);
                return;
            }
            if (Bag.RemoveItems(priceItem, 1))
            {
                Debug.Log("Meat taken");
                StoryStateManager.SetFlag(eventId);
                DialogueManager.Instance.StartDialogue(dialogueData);
            }
            else
            {
                DialogueManager.Instance.StartDialogue(notEnoughCostDialogue);
                Debug.Log("Not enough meat.");
            }
        }
    
        public string InteractText()
        {
            return interactText;
        }
    }
}
