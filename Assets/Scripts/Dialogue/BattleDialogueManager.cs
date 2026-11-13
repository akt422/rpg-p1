using UnityEngine;

public class BattleDialogueManager : MonoBehaviour
{
    public static BattleDialogueManager Instance { get; private set; }
    [SerializeField] private GameObject dialoguePanel;
    private bool isTalking;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        dialoguePanel.SetActive(true);
    }
    
}