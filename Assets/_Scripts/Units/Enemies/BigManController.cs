using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class BigManController : MonoBehaviour
{
    public float walkSpeed = 3f;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    Vector2 walkDirectionVector = Vector2.right;

    public enum WalkableDirection { Left, Right };

    private WalkableDirection _walkableDirection;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if(WalkDirection == WalkableDirection.Right) 
        { WalkDirection = WalkableDirection.Left;} else
        {
            Debug.LogError("Current walkable direction is not set to legal value.");
        }
    }
}
