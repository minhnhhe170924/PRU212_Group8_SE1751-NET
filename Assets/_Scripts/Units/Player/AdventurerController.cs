using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class AdventurerController : PlayerUnitBase
{
    public float walkSpeed = 5.0f;
    public float runSpeed = 8.0f;
    public float jumpImpulse = 9f;
    public float airWalkSpeed = 3.0f;

    public float switchGravityTimeLimit = 10.0f;
    private float currentSwitchGravityCooldown = 0.0f;
    private bool isSwitchGravityActive = false;

    private bool canDash = true;
    private bool isDashing = false;

    [SerializeField] private float dashPower = 30f;
    [SerializeField] private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;

    [SerializeField]
    private TrailRenderer dashingTrail;

    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public float CurrentMoveSpeed
    {
        get
        {
            if (!CanMove)
            {
                return 0;
            }

            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (touchingDirections.IsGrounded)
                {
                    if (IsRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                else
                {
                    return airWalkSpeed;
                }
            }
            else
            {
                return 0;
            }
        }
    }


    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    [SerializeField]
    private bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                // Flip the direction of player
                this.transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }




    Rigidbody2D rb;
    Animator animator;

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
        private set { }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        dashingTrail = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

            // Check for cooldown of switch gravity skill
            if (isSwitchGravityActive)
            {
                if (currentSwitchGravityCooldown > 0.0f)
                {
                    currentSwitchGravityCooldown -= Time.deltaTime;
                }
                else
                {
                    SwitchGravity();
                    isSwitchGravityActive = false;
                }
            }
        }

        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face to the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face to the left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && CanMove && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);

            if(isSwitchGravityActive)
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpImpulse);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void OnSwitchGravity(InputAction.CallbackContext context)
    {
        if (context.started && !isSwitchGravityActive && CanMove && touchingDirections.IsGrounded)
        {
            SwitchGravity();
            isSwitchGravityActive = true;

            // Start cooldown of switch gravity skill
            currentSwitchGravityCooldown = switchGravityTimeLimit;
        }
    }

    private void SwitchGravity()
    {
        animator.SetTrigger(AnimationStrings.switchGravityTrigger);
        rb.gravityScale *= -1;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && CanMove && canDash)
        {
            animator.SetTrigger(AnimationStrings.dashTrigger);
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        
        float oriGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        dashingTrail.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        dashingTrail.emitting = false;
        rb.gravityScale = oriGravityScale;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }
}
