using Interactions;
using UnityEngine;

public class StoolInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorInteract door;
    private string interactText = "E - Interact";
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        door.setLock(false);
        Debug.Log("Door unlocked!");
    }
    
    public string InteractText()
    {
        return interactText;
    }
}