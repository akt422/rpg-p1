using Interactions;
using UnityEngine;

public class BedInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "E - Rest";
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
        SceneTransitionManager.Instance.LoadScene("start-room");
    }

    public string InteractText()
    {
        return interactText;
    }

    public Transform InteractPosition() => interactPosition;
}