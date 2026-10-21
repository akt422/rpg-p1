using UnityEngine;
using UnityEngine.InputSystem;

public class SupportCombatController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpSpeed = 8f;
    private Vector2 _moveInput;
    private Animator animator; 
    private bool isJumping = false;
    private PlayerCombatObject player;
    
    private Rigidbody2D rb;

    void Awake()
    {
        GameStateManager.SetState(GameState.Combat); // todo: remove
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerCombatObject>();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        bool isWalking = _moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            animator.SetFloat("walk_dir", _moveInput.x);
        }
    }

    void OnJump(InputValue value)
    {
        if (!value.isPressed || isJumping)
            return;

        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    void OnBlock(InputValue value)
    {
        player.IsBlocking = value.isPressed;
        animator.SetBool("isBlocking", player.IsBlocking);
    }
    
    void FixedUpdate()
    {
        if (GameStateManager.GetState() != GameState.Combat || player.IsBlocking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = new Vector2(_moveInput.x * moveSpeed, rb.linearVelocity.y);
        // if (rb.linearVelocity.y != 0f) isJumping = true;
        // else isJumping = false;
    }
}
