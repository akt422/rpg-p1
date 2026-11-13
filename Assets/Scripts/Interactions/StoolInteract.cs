using System;
using Interactions;
using UnityEngine;

public class StoolInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorInteract door;
    private string interactText = "E - Interact";
    [SerializeField] private Transform interactPosition;

    public Transform InteractPosition() => interactPosition;

    private void Awake()
    {
        if(interactPosition == null) interactPosition = transform;
    }

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