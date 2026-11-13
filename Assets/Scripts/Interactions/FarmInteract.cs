using Interactions;
using UnityEngine;
// ReSharper disable All

public class FarmInteract : MonoBehaviour, IInteractable
{
    private string interactText = "Press E - Farm mode";
    [SerializeField] private FarmField farmField;
    private FarmController farmController;
    [SerializeField] private Transform interactPosition;

    public Transform InteractPosition() => interactPosition;

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
        if (GameStateManager.GetState() == GameState.Gameplay)
        {
            FarmController.Instance.StartFarmMode(Bag.Items, farmField);
        }
        else
            GameStateManager.SetState(GameState.Gameplay);
    }

    public string InteractText()
    {
        return interactText;
    }
}