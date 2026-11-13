using System.Collections;
using Dialogue;
using Interactions;
using UnityEngine;

public abstract class ItemInteract : MonoBehaviour, IInteractable
{
    private bool collected = false;
    [SerializeField] private string pickUpText;
    [SerializeField] private AudioClip keyPickClip;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private ItemData item;
    private Animation animation;
    [SerializeField] private Transform interactPosition;

    public Transform InteractPosition() => interactPosition;

    void Awake()
    {
        animation = GetComponent<Animation>();
        if(interactPosition == null) interactPosition = transform;
    }
    
    public bool CanInteract()
    {
        return !collected;
    }

    public void Interact()
    {
        if(collected) return;
        collected = true;
        Bag.AddItem(item, 1);
        AudioManager.Instance.PlaySound(keyPickClip);
        DialogueManager.Instance.StartDialogue(dialogueData);
        if (animation != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(PlayAnimation());
        }
        else gameObject.SetActive(false);
    }

    public abstract string InteractText();

    private IEnumerator PlayAnimation()
    {
        animation.Play(animation.clip.name);
        yield return new WaitWhile(() => animation.isPlaying);
        gameObject.SetActive(false);
    }
} 