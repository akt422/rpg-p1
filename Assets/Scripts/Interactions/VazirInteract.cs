using System;
using Dialogue;
using Interactions;
using UnityEngine;
// ReSharper disable All

public class VazirInteract : MonoBehaviour, IInteractable
{
    private string interactText = "E - Talk";
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private Transform interactPosition;
    [SerializeField] private Animator gateAnimator;
    private Animator vazirAnimator;
    private string event_id = "start-gate-open";

    public Transform InteractPosition() => interactPosition;

    void Awake()
    {
        if(interactPosition == null) interactPosition = transform;
        vazirAnimator =  GetComponent<Animator>();
    }

    void Update()
    {
        if (vazirAnimator.GetBool("triggerGate"))
        {
            gateAnimator.SetBool("triggerGate", true);
        }
    }

    private void OnEnable()
    {
        DialogueManager.Instance.dialogueActionTriggered += GateOpen;
    }

    private void OnDisable()
    {
        DialogueManager.Instance.dialogueActionTriggered -= GateOpen;

    }

    public bool CanInteract()
    {
        return transform.position.x >= 0.01f;
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueData);
        
    }

    public string InteractText()
    {
        return interactText;
    }

    private void GateOpen(string eventId)
    {
        if (eventId == event_id)
        {
            vazirAnimator.SetBool("triggerWalk", true);
        }
    }
}