using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private SpriteRenderer _sr;
    private float _MoveX;
    private bool _bIsAlive;
    private Animator animator;

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling
    }

    private MovementState state = MovementState.idle;
    
    bool isGrounded;
    [SerializeField] private float JumpHeight;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform FeetPos;
    [SerializeField] private float CheckRadius;
    private void Start()
    {
        _bIsAlive = true;
        rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        if(_bIsAlive)
        {
            MovementState state;
            KeyMovement();
            isGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, whatIsGround);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (rb.velocity.x > 0.0f)
            {
                _sr.flipX = false;
                state = MovementState.running;
            }
            else if (rb.velocity.x < 0.0f)
            {
                _sr.flipX = true;
                state = MovementState.running;
            }
            else
            {
                state = MovementState.idle;
            }

            if (rb.velocity.y > .1)
            {
                state = MovementState.jumping;
            }
            else if (rb.velocity.y < -.1)
            {
                state = MovementState.falling;
            }
            
            animator.SetInteger("state", (int)state);
            
        }
        
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_MoveX * speed, rb.velocity.y);
    }

    private void KeyMovement()
    {
        _MoveX = Input.GetAxisRaw("Horizontal");
    }

    private void Jump()
    {
        
        rb.velocity = Vector2.up * JumpHeight;

    }
}
