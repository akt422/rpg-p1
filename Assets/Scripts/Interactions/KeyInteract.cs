using Dialogue;
using UnityEngine;

namespace Interactions
{
    public class KeyInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueData dialogueData;
        [SerializeField] private string[] dialogueText;
        private GameObject keyObject;
        [SerializeField] private DoorInteract2 door;
        private bool collected = false;
        [SerializeField] private AudioClip keyPickClip;
        private string interactText = "E - Pick up";

        public bool CanInteract()
        {
            return !collected;
        }

        public void Interact()
        {
            if (collected)
                return;
        
            AudioManager.Instance.PlaySound(keyPickClip);
            collected = true;
            DialogueManager.Instance.StartDialogue(dialogueData);
            gameObject.SetActive(false);
            door.Unlock();
        }

        public string InteractText()
        {
            return interactText;
        }
    }
}
