using UnityEngine;

public class KeyHouse : ItemInteract
{
    private string interactText = "E - Pick up key";
    public override string InteractText()
    {
        return interactText;
    }
}