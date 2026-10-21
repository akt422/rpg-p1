using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float move_speed = 4f;
    private Rigidbody2D rb;
    private Vector2 _moveInput;
    private Animator animator; 
    private float dir = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = _moveInput * move_speed;
    }

    public void OnMove(InputValue value) {
        _moveInput = value.Get<Vector2>();

        bool isWalking = _moveInput.sqrMagnitude > 0.01f;
        if ((dir == 1 && _moveInput.x < 0) ||
            (dir == -1 && _moveInput.x > 0))
        {
            Flip();
        }

        animator.SetBool("isWalking", isWalking);
    }

    private void Flip()
    {
        dir *= -1;
        transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z
        );
    }
    
}
