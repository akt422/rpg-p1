using Interactions;
using Quest;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitchInteract : MonoBehaviour, IInteractable
{
    private Light2D light;
    private string interactText = "E - Switch on/off";
    private AudioSource audioSource;
    [SerializeField] private AudioClip lightAudio;
    private bool switchedOnOnce = false;
    [SerializeField] private QuestData questData;
    [SerializeField] private QuestProgress progress;
    
    void Awake()
    {
        audioSource = AudioManager.Instance.GetComponent<AudioSource>();
        light = gameObject.GetComponent<Light2D>();
        light.enabled = false;
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        light.enabled = !light.enabled;
        audioSource.PlayOneShot(lightAudio);
        QuestEventBus.RaiseEvent("switch_on_light");
    }

    public string InteractText()
    {
        return interactText;
    }
}
