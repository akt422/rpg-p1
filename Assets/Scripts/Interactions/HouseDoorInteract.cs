using Dialogue;
using UnityEngine;

namespace Interactions
{
    public class HouseDoorInteract : MonoBehaviour, IInteractable
    {
        bool isOpened = false;
        private string interactText = "E - Try to open";
        [SerializeField] private ItemData keyToUnlock;
        [SerializeField] private DialogueData keyNotPresentDialogue;
        private AudioSource audioSource;
        private DialogueManager dialogueManager;
        [SerializeField] private AudioClip openDoorClip;
        [SerializeField] private DialogueData unlockDoorData;
    
        private void Awake()
        {
            audioSource = AudioManager.Instance.GetComponent<AudioSource>();
            dialogueManager = DialogueManager.Instance;
        }
        public bool CanInteract()
        {
            return !isOpened;
        }

        public void Interact()
        {
            if (!CanInteract()) return;
            if (Bag.HasItem(keyToUnlock))
            {
                isOpened = true;
                gameObject.SetActive(false);
                audioSource.PlayOneShot(openDoorClip);
                dialogueManager.StartDialogue(unlockDoorData);
            }
            else
            {
                dialogueManager.StartDialogue(keyNotPresentDialogue);
            }
        }

        public string InteractText()
        {
            return interactText;
        }
    }
}