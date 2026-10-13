using Dialogue;
using UnityEngine;

namespace Interactions
{
    public class Npc2Interact : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueData dialogueData;
        [SerializeField] private string eventId;
        private string interactText = "E - Talk";
    
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
    }
}
