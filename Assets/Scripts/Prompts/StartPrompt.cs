using UnityEngine;

public class StartPrompt : MonoBehaviour
{
    [SerializeField] private string StartGameText = "Press E to breathe life";
    private PromptManager promptManager;

    void Awake()
    {
        promptManager = PromptManager.Instance;
    }
    
    public void StartGamePrompt()
    {
        promptManager.StartPrompt(StartGameText);
    }
}