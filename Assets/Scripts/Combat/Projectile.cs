using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed_min = 5f;
    [SerializeField] private float speed_max = 15f;
    [Header("Phase 1: Upward Launch")]
    [SerializeField] private float launchUpSpeed = 8f;
    [SerializeField] private float targetHeightOffset = 14f; // How high above spawn to climb
    [SerializeField] private float horizontalSpread = 0.5f; // Horizontal variance (-0.5 to 0.5)

    [Header("Phase 2: Target Boost")]
    [SerializeField] private float hoverDelayMin = 0.5f; // Pause duration min at peak before diving
    [SerializeField] private float hoverDelayMax = 1.5f; // Pause duration max at peak before diving
    [SerializeField] private float boostSpeed = 18f;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip boomClip;
    public event Action hitAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = AudioManager.Instance.GetComponent<AudioSource>();
    }
    
    public void Initialize(Transform target)
    {
        StartCoroutine(LaunchAndTargetRoutine(target));
    }
    
    private IEnumerator LaunchAndTargetRoutine(Transform target)
    {
        float startY = transform.position.y;
        float endY = startY + targetHeightOffset;

        // Generate a vector pointing predominantly upward with random horizontal tilt
        Vector2 randomUpDir = new Vector2(Random.Range(-horizontalSpread, horizontalSpread), 1f).normalized;
        rb.AddForce(randomUpDir * launchUpSpeed, ForceMode2D.Impulse);
        
        // Wait until projectile climbs past target height OR starts falling
        yield return new WaitUntil(() => transform.position.y >= endY || rb.linearVelocity.y <= 0f);
        
        // Stop mid-air
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        float currGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if (hoverDelayMin > 0)
        {
            yield return new WaitForSeconds(Random.Range(hoverDelayMin, hoverDelayMax));
        }
        
        rb.gravityScale = currGravity;
        
        float randomSpeed = Random.Range(speed_min, speed_max) / transform.localScale.x;
        if (target == null)
        {
            Debug.LogWarning("Player not found");
            yield break;
        }
        Vector2 directionToPlayer = (target.position - transform.position).normalized;
        float spreadFactor = 0.15f; // this and below are to have some spread to avoid shooting at player perfectly always.
        Vector2 randomizedDirection = (directionToPlayer + (Random.insideUnitCircle * spreadFactor)).normalized;
        
        rb.AddForce(randomizedDirection * randomSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(boomClip);
            Destroy(gameObject);
            hitAction?.Invoke();
        }
    }
}