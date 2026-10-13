using Interactions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitchInteract : MonoBehaviour, IInteractable
{
    private Light2D light;
    private string interactText = "E - Switch on/off";
    private AudioSource audioSource;
    [SerializeField] private AudioClip lightAudio;
    
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
    }

    public string InteractText()
    {
        return interactText;
    }
}
