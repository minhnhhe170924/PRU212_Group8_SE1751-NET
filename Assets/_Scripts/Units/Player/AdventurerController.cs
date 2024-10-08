using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class AdventurerController : PlayerUnitBase {
    //[SerializeField] private AudioClip _someSound;

    //void Start() {
    //    // Example usage of a static system
    //    AudioSystem.Instance.PlaySound(_someSound);
    //}

    //public override void ExecuteMove() {
    //    // Perform tarodev specific animation, do damage, move etc.
    //    // You'll obviously need to accept the move specifics as an argument to this function. 
    //    // I go into detail in the Grid Game #2 video
    //    base.ExecuteMove(); // Call this to clean up the move
    //}

    public float walkSpeed = 5.0f;

    Vector2 moveInput;
    
    public bool IsMoving { get; private set; }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;


    }
}
