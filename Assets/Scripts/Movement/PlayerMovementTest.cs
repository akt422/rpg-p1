using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTest : MonoBehaviour
{
    private Vector2 _moveInput;
    [SerializeField] private float move_speed = 6f;
    private Rigidbody2D rb;

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
        rb.linearVelocity = _moveInput * move_speed;
    }
}
