using System.Collections;
using UnityEngine;

public class CombatHeroWeapon : MonoBehaviour
{

    [SerializeField] private AudioClip audioClip;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = AudioManager.Instance.GetComponent<AudioSource>();
    }
    
    public void Initialize()
    {
        StartCoroutine(LaunchRoutine());
    }

    private IEnumerator LaunchRoutine()
    {
        rb.AddForce(Vector2.up * 50f, ForceMode2D.Impulse);
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}