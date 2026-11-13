using System;
using UnityEngine;

public class TriggerBattleDialogue : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            BattleDialogueManager.Instance.StartDialogue(dialogueData);
        }
    }
}