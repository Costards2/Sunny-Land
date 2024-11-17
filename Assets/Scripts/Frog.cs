using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Collider2D collider2D;
    private AudioSource audioSource;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
            Dead();
            audioSource.Play();
            collider2D.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}
