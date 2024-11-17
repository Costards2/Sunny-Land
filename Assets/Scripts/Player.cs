using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float jumpYVelocity = 15f;
    [SerializeField] float velocity = 8f;

    private float horizontalInput;

    Animator animator;
    Rigidbody2D physics;
    SpriteRenderer sprite;

    Vector3 initialPosition;

    enum State { Idle, Run, Jump, Fall }
    State state = State.Idle;

    bool isGrounded = false;
    bool jumpInput = false;

    void Awake()
    {
        Cursor.visible = false;
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
        physics = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        jumpInput = Input.GetKey(KeyCode.Space);
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Flip sprite based on input
        if (horizontalInput < 0) sprite.flipX = true;
        else if (horizontalInput > 0) sprite.flipX = false;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle: IdleState(); break;
            case State.Run: RunState(); break;
            case State.Jump: JumpState(); break;
            case State.Fall: FallState(); break;
        }
    }

    void IdleState()
    {
        animator.Play("Idle");
        physics.velocity = new Vector2(0, physics.velocity.y);

        if (jumpInput && isGrounded)
        {
            state = State.Jump;
        }
        else if (horizontalInput != 0)
        {
            state = State.Run;
        }
        else if (!isGrounded)
        {
            state = State.Fall;
        }
    }

    void RunState()
    {
        if (horizontalInput != 0)
        {
            animator.Play("Run");
            physics.velocity = new Vector2(velocity * horizontalInput, physics.velocity.y);
        }

        if (jumpInput && isGrounded)
        {
            state = State.Jump;
        }
        else if (horizontalInput == 0)
        {
            state = State.Idle;
        }
        else if (!isGrounded)
        {
            state = State.Fall;
        }
    }

    void JumpState()
    {
        animator.Play("Jump");
        physics.velocity = new Vector2(physics.velocity.x, jumpYVelocity);
        state = State.Fall;
    }

    void FallState()
    {
        animator.Play("Fall");

        physics.velocity = new Vector2(velocity * horizontalInput, physics.velocity.y);

        if (isGrounded)
        {
            if (horizontalInput == 0)
            {
                state = State.Idle;
            }
            else
            {
                state = State.Run;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            transform.position = initialPosition;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            physics.AddForce(new Vector2(physics.velocity.x, 200f), ForceMode2D.Impulse);
            //physics.velocity = new Vector2(physics.velocity.x, jumpYVelocity);
        }   
        else if (collision.CompareTag("Fall"))
        {
            transform.position = initialPosition;
        }
    }
}
