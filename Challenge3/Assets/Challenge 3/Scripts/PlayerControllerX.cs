/*
 * Zach Daly
 * Assignment 4
 * Manages player controls and effects
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public ForceMode forceMode;
    public float floatForce, maxHeight = 15.0f;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle, fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound, explodeSound;

    private UIManagerX uIManager;

    void Start()
    {
        // Set ref variables to components
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Set gravity
        if (Physics.gravity.y > -10)
            Physics.gravity *= gravityModifier;

        // Set force mode
        forceMode = ForceMode.Impulse;

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, forceMode);
    }

    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && transform.position.y < maxHeight)
        {
            playerRb.AddForce(Vector3.up * floatForce, forceMode);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }

        // If player hits floor, push back up
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * floatForce, forceMode);
        }
    }
}