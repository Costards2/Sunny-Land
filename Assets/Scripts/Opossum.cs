using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    public float walkSpeed = 5f;
    public Transform pointRight;
    public Transform pointLeft;

    private float right;
    private float left;
    private SpriteRenderer sprite;
    private Collider2D collider2D; 
    private AudioSource audioSource;    

    private enum EnemyState
    {
        WalkingRight,
        WalkingLeft,
        Dead
    }

    private EnemyState currentState;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        right = pointRight.position.x;
        left = pointLeft.position.x;
        currentState = EnemyState.WalkingRight;

        sprite.flipX = true;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.WalkingRight:
                WalkRight();
                break;
            case EnemyState.WalkingLeft:
                WalkLeft();
                break;
            case EnemyState.Dead:
                Dead(); 
                break;
        }
    }

    void WalkRight()
    {
        transform.Translate(Time.deltaTime * walkSpeed * Vector2.right);

        if (transform.position.x >= right)
        {
            currentState = EnemyState.WalkingLeft;
            sprite.flipX = false; // Flip the sprite to face left
        }
    }

    void WalkLeft()
    {
        transform.Translate(Time.deltaTime * walkSpeed * Vector2.left);

        if (transform.position.x <= left)
        {
            currentState = EnemyState.WalkingRight;
            sprite.flipX = true; // Flip the sprite to face right
        }
    }

    void Dead()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentState = EnemyState.Dead;
            audioSource.Play();
            collider2D.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}
