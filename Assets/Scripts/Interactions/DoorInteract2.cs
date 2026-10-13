using Dialogue;
using UnityEngine;

namespace Interactions
{
    public class DoorInteract2 : MonoBehaviour, IInteractable
    {
        private bool isLocked = true;
        private bool isOpened = false;
        [SerializeField] private DialogueData dialogueData;
        [SerializeField] private DialogueData unlockDoorData;
        [SerializeField] private string[] dialogueText;
        [SerializeField] private Sprite openDoor;
        private SpriteRenderer sr;
        private BoxCollider2D bc;
        [SerializeField] private AudioClip lockedDoorClip;
        [SerializeField] private AudioClip openDoorClip;
        private AudioSource audioSource;
        [SerializeField] private ItemData keyToUnlock;
        private string interactText = "E - Try to open";

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            bc = GetComponent<BoxCollider2D>();
            audioSource = AudioManager.Instance.GetComponent<AudioSource>();
        }
        public bool CanInteract()
        {
            return !isOpened;
        }

        public void Interact()
        {
            if (isLocked)
            {
                if (isKeyPresent(keyToUnlock))
                {
                    isLocked = false;
                    audioSource.PlayOneShot(openDoorClip);
                    DialogueManager.Instance.StartDialogue(unlockDoorData);
                    sr.sprite = openDoor;
                    bc.isTrigger = true;
                    isOpened = true;
                }
                else
                {
                    audioSource.PlayOneShot(lockedDoorClip);
                    DialogueManager.Instance.StartDialogue(dialogueData);
                }
            }
            else
            {
                isOpened = true;
            }
        }

        private bool isKeyPresent(ItemData key)
        {
            return Bag.HasItem(key);
        }

        public string InteractText()
        {
            return interactText;
        }

        public void SetLock(bool val)
        {
            isLocked = val;
        }

        public void Unlock()
        {
            isLocked = false;
        }

        public bool GetLock()
        {
            return isLocked;
        }
    }
}
