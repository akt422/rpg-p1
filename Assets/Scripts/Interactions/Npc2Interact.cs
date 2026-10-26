using Dialogue;
using UnityEngine;

namespace Interactions
{
    public class Npc2Interact : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueData dialogueData;
        [SerializeField] private string eventId;
        private string interactText = "E - Talk";
        [SerializeField] private Quest.QuestData questData;
        [SerializeField] private QuestProgress initialProgress;
        [SerializeField] private QuestProgress finalProgress;
    
        public bool CanInteract()
        {
            return true;
        }

        public void Interact()
        {
            DialogueManager.Instance.StartDialogue(dialogueData);
            // if (Quest.Quest.GetQuestProgress(questData) == initialProgress)
            // {                
            //     Quest.Quest.SetQuestProgress(questData, finalProgress);
            //     return;
            // }
            if (!Quest.Quest.DoesStateExist(questData))
            {
                Quest.Quest.AddQuest(questData);
            }            
            QuestEventBus.RaiseEvent("talk_npc_switch");
        }
    
        public string InteractText()
        {
            return interactText;
        }
    }
}
