using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTest : MonoBehaviour
{
    private Vector2 _moveInput;
    [SerializeField] private float move_speed = 6f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool right = true;
    [SerializeField] private GameObject lamp;
    private SpriteRenderer spriteRenderer;

    // void Update()
    // {
    //     if (Keyboard.current.escapeKey.wasPressedThisFrame)
    //     {
    //         GameStateManager.SetState(GameStateManager.GetState() != GameState.Pause
    //             ? GameState.Pause
    //             : GameState.Gameplay);
    //     }
    // }
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (GameStateManager.GetState() != GameState.Gameplay)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if ((right && _moveInput.x < 0) || (!right && _moveInput.x > 0))
            Flip();
        bool isWalking = _moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);
        rb.linearVelocity = _moveInput * move_speed;
    }

    private void Flip()
    {
        right = !right;
        // gameObject.transform.localScale = new Vector3(
        //     -transform.localScale.x,
        //     transform.localScale.y,
        //     transform.localScale.z
        // );
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
