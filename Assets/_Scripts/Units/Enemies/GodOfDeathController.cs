using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class GodOfDeathController : MonoBehaviour
{
    public float walkAcceleration = 30f;
    public float maxSpeed = 3f;

    private bool canCastSkill = false;

    [SerializeField] private float castSkillPower = 30f;
    [SerializeField] private float castSkillingTime = 0.1f;
    [SerializeField] private int castingSkillCooldownMin = 4;
    [SerializeField] private int castingSkillCooldownMax = 7;

    public GameObject GODSkillPrefab;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;
    Vector2 walkDirectionVector = Vector2.left;

    public enum WalkableDirection { Right, Left };

    private WalkableDirection _walkableDirection = WalkableDirection.Left;

    public WalkableDirection WalkDirection
    {
        get { return _walkableDirection; }
        set
        {
            if (_walkableDirection != value)
            {
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                    );

                if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
                else if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
            }
            _walkableDirection = value;
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);

        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall || cliffDetectionZone.detectedColliders.Count == 0)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded)
            {
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime),
                    -maxSpeed,
                    maxSpeed),
                    rb.velocity.y);

                if(canCastSkill)
                {
                    animator.SetTrigger(AnimationStrings.castSkillTrigger);
                    StartCoroutine(CastSkill());
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal value.");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        if (damageable.Health <= damageable.MaxHealth / 2)
        {
            canCastSkill = true;
        }

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCLiffDeteced()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }

    public void OnCastSkill(InputAction.CallbackContext context)
    {
        if (context.started && CanMove && canCastSkill)
        {
            animator.SetTrigger(AnimationStrings.castSkillTrigger);
            StartCoroutine(CastSkill());
        }
    }

    private IEnumerator CastSkill()
    {
        canCastSkill = false;

        GameObject player = GameObject.Find("Player");
        System.Random rnd = new System.Random();
        double rndX = rnd.NextDouble() * 5;

        Transform laucnhPoint = player.transform;
        Vector3 newPos = new Vector3(laucnhPoint.position.x + (float)rndX, gameObject.transform.position.y + 1, laucnhPoint.position.z);
        GameObject GODSkill = Instantiate(GODSkillPrefab, newPos, GODSkillPrefab.transform.rotation);

        yield return new WaitForSeconds(castSkillingTime);

        yield return new WaitForSeconds(rnd.Next(castingSkillCooldownMin, castingSkillCooldownMax));

        canCastSkill = true;
    }
}
