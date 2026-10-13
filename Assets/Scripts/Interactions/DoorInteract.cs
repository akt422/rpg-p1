using UnityEngine;

namespace Interactions
{
    public class DoorInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private Sprite openDoor;
        [SerializeField] private Sprite closeDoor;
        private SpriteRenderer sr;
        private BoxCollider2D bc;
        private bool isOpened = false;
        private bool isLocked = true;
        private string interactText = "E - Try to open";
        private string _interactText;

        public void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            bc = GetComponent<BoxCollider2D>();
        }
        public bool CanInteract()
        {
            return true;
        }
    
        public bool getLock() { return isLocked; }
        public void setLock(bool key) { isLocked = key; }

        public void Interact()
        {
            if (isLocked)
            {
                Debug.Log("Locked door, try again later.");
                return;
            }
            isOpened = !isOpened;
            if (isOpened)
            {
                sr.sprite = openDoor;
                bc.isTrigger = true;
            }
            else
            {
                sr.sprite = closeDoor;
                bc.isTrigger = false;
            }
        }

        public string InteractText()
        {
            return interactText;
        }
    }
}