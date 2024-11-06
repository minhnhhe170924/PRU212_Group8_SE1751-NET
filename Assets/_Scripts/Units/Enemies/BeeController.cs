using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public float flySpeed = 2f;

    Rigidbody2D rb;
    public DetectionZone attackZone;
    Animator animator;
    Damageable damageable;
    public Collider2D deathCollider;

    public List<Transform> waypoints;
    public float waypointReachedDistance = 0.1f;
    Transform nextWaypoint;
    int waypointNum = 0;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath);
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Flight()
    {
        Vector2 directionToNextWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(transform.position, nextWaypoint.position);

        rb.velocity = directionToNextWaypoint * flySpeed;

        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;

        if (localScale.x < 0 && rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
        else if (localScale.x > 0 && rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
    }

    public void OnDeath()
    {
        rb.gravityScale = 2f;
        transform.rotation = new Quaternion(0, 0, -45, 0);
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
