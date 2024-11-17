using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Collider2D collider2D;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
