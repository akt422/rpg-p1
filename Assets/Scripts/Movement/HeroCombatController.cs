using UnityEngine;
using UnityEngine.InputSystem;

public class HeroCombatController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpSpeed = 8f;
    private Vector2 _moveInput;
    // private Animator animator; 
    private bool isJumping = false;
    private PlayerCombatObject player;
    private Rigidbody2D rb;
    private bool isMeditating;
    private float meditationTime = 0f;
    [SerializeField] private float meditationDuration = 5f;
    private float activeTime = 0f;
    [SerializeField] private float activeDuration = 5f;
    [SerializeField] private GameObject heroObject;
    [SerializeField] private GameObject supportObject;
    [SerializeField] private GameObject weaponPrefab;
    private PlayerInput playerInput;
    private PlayerInput supportInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
        player = GetComponent<PlayerCombatObject>();
        isMeditating = true;
        playerInput = GetComponent<PlayerInput>();
        supportInput = supportObject.GetComponent<PlayerInput>();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        bool isWalking = _moveInput.sqrMagnitude > 0.01f;
        // animator.SetBool("isWalking", isWalking);
        // if (isWalking)
        // {
        //     animator.SetFloat("walk_dir", _moveInput.x);
        // }
    }

    void OnJump(InputValue value)
    {
        if (!value.isPressed || isJumping || isMeditating)
            return;

        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    void OnAttack()
    {
        if (isMeditating) return;
        CombatHeroWeapon weaponObj = Instantiate(weaponPrefab, transform.position,  Quaternion.identity).GetComponent<CombatHeroWeapon>();
        weaponObj.Initialize();
        EnterMeditationPhase();
    }

    void Update()
    {
        if (GameStateManager.GetState() != GameState.Combat)
            return;

        if (isMeditating)
        {
            meditationTime += Time.deltaTime;

            if (meditationTime >= meditationDuration)
                EnterActivePhase();
        }
        else
        {
            activeTime += Time.deltaTime;

            if (activeTime >= activeDuration)
                EnterMeditationPhase();
        }
    }
    void FixedUpdate()
    {
        if (GameStateManager.GetState() != GameState.Combat || player.IsBlocking || isMeditating)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = new Vector2(_moveInput.x * moveSpeed, rb.linearVelocity.y);
        // if (rb.linearVelocity.y != 0f) isJumping = true;
        // else isJumping = false;
    }
    
    private void EnterActivePhase()
    {
        isMeditating = false;
        meditationTime = 0f;

        player.IsBlocking = false;

        supportInput.enabled = false;
        playerInput.enabled = true;
    }

    private void EnterMeditationPhase()
    {
        isMeditating = true;
        activeTime = 0f;

        player.IsBlocking = false;
        _moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        
        supportInput.enabled = true;
        playerInput.enabled = false;
    }
}
