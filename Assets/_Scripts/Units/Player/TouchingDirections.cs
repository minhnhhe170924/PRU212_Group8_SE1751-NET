using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    Animator animator;

    [SerializeField]
    private bool _isGrounded = true;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    public bool GravitySwitched
    {
        get
        {
            bool gravitySwitched = gameObject.GetComponent<Rigidbody2D>().gravityScale < 0;

            animator.SetBool(AnimationStrings.gravitySwitched, gravitySwitched);
            return gravitySwitched;
        }
    }

    private Vector2 wallCheckDirection
    {
        get
        {
            int checkVal = 0;

            if (gameObject.CompareTag("GOD"))
            {
                checkVal = 1;
            }
            else if (gameObject.CompareTag("Knight"))
            {
                checkVal = -1;
            }
            else if (gameObject.CompareTag("BigMan"))
            {
                checkVal = -1;
            }

            return gameObject.transform.localScale.x == checkVal ? Vector2.left : Vector2.right;
        }
    }
    private Vector2 groundCheckDirection
    {
        get
        {
            if (GravitySwitched)
            {
                return Vector2.up;
            }
            else
            {
                return Vector2.down;
            }
        }
    }

    private Vector2 ceilingCheckDirection => groundCheckDirection == Vector2.down ? Vector2.up : Vector2.down;

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(groundCheckDirection, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(ceilingCheckDirection, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
