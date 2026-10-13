using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// todo: fix dialogue in cases without sprite or speaker name
namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TMP_Text dialogueText;
        private DialogueEntry[] currentDialogueEntries;
        [SerializeField] private Image speakerSprite;
        [SerializeField] private TMP_Text dialogueSpeakerName;
        [SerializeField] private ChoiceController choiceController;
        private DialogueChoice[] choices;
        private int currLine = 0;
        [SerializeField] private float typeSpeed = 0.3f;
        private bool isTyping = false;
        private bool isChoosing = false;
        private Coroutine typingCoroutine;
        [SerializeField] private AudioClip talkingClip;
        public event Action<string> dialogueActionTriggered;
        private string eventId;
        public static DialogueManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private IEnumerator TypeLine(string text)
        {
            isTyping = true;
            dialogueText.text = "";
            foreach (char c in text)
            {
                AudioManager.Instance.PlaySound(talkingClip);
                dialogueText.text += c;
                yield return new WaitForSeconds(typeSpeed);
            }
            isTyping = false;
        }

        // public void StartDialogue(string[] text)
        // {
        //     currLine = 0;
        //     currentDialogue = text;
        //     GameStateManager.SetState(GameState.Dialogue);
        //     dialoguePanel.SetActive(true);
        //     typingCoroutine = StartCoroutine(TypeLine(currentDialogue[currLine]));
        // }
        
        public void StartDialogue(DialogueData dialogueData)
        {
            currLine = 0;
            isChoosing = false;
            currentDialogueEntries = dialogueData.Entries;
            choices = dialogueData.Choices;
            eventId = dialogueData.EventId;
            GameStateManager.SetState(GameState.Dialogue);
            dialoguePanel.SetActive(true);
            DisplayCurrentEntry();
        }

        private void DisplayCurrentEntry()
        {
            DialogueEntry currentDialogueEntry = currentDialogueEntries[currLine];
            speakerSprite.sprite = currentDialogueEntry.DialogueSpeaker.SpeakerSprite;
            dialogueSpeakerName.text = currentDialogueEntry.DialogueSpeaker.SpeakerName;
            typingCoroutine = StartCoroutine(TypeLine(currentDialogueEntry.Text));
        }

        public void AdvanceDialogue()
        {
            if (isChoosing) return;
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentDialogueEntries[currLine].Text;
                isTyping = false;
                return;
            }
            currLine++;
            if (currLine < currentDialogueEntries.Length)
            {
                DisplayCurrentEntry();
            }
            else
            {
                if (choices.Length > 0)
                {
                    isChoosing = true;
                    choiceController.InstantiateChoices(choices);
                }
                else
                {
                    EndDialogue();
                }
            }
        }

        private void EndDialogue()
        {
            GameStateManager.SetState(GameState.Gameplay);
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            if (!string.IsNullOrWhiteSpace(eventId))
            {
                dialogueActionTriggered?.Invoke(eventId);
            }
        }
    }
}
