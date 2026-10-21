using UnityEngine;

public class PlayerModeController : MonoBehaviour
{
    private PlayerMovementTest movement;
    private InteractionDetector interactionDetector;
    private Rigidbody2D rb;
    private SpriteRenderer[] renderers;
    private Collider2D[] colliders;

    private void Awake()
    {
        movement = GetComponent<PlayerMovementTest>();
        interactionDetector = GetComponent<InteractionDetector>();
        rb = GetComponent<Rigidbody2D>();

        renderers = GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponentsInChildren<Collider2D>();
    }

    public void SetExplorationActive(bool active)
    {
        movement.enabled = active;
        interactionDetector.enabled = active;

        foreach (SpriteRenderer sr in renderers)
            sr.enabled = active;

        foreach (Collider2D col in colliders)
            col.enabled = active;

        if (!active)
            rb.linearVelocity = Vector2.zero;
    }
}