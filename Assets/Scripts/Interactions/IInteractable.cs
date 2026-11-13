using UnityEngine;

namespace Interactions
{
    public interface IInteractable
    {
        bool CanInteract();
        void Interact();
        string InteractText();
        Transform InteractPosition();
    }
}