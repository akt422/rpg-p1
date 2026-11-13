using Interactions;
using UnityEngine;

public class PotInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "E - Examine";
    [SerializeField] private Transform interactPosition;

    void Awake()
    {
        if(interactPosition == null) interactPosition = transform;
    }
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        Debug.Log("Potttt");
    }

    public string InteractText()
    {
        return interactText;
    }

    public Transform InteractPosition() => interactPosition;
}