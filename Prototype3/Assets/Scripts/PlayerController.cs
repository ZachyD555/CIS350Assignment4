/*
 * Zach Daly
 * Assignment 4
 * Controls player actions, sounds, particles, and game over state
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to player
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public ForceMode jumpForceMode;
    public float jumpForce, gravityModifier;
    public bool isOnGround = true, gameOver = false;
    private Animator playerAnimator;
    public ParticleSystem explosionParticle, dirtParticle;
    public AudioClip jumpSound, crashSound;
    private AudioSource playerAudio;

    void Start()
    {
        // Set ref variables to components
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();

        // Modify gravity
        if (Physics.gravity.y > -10)
            Physics.gravity *= gravityModifier;

        // Set the force mode
        jumpForceMode = ForceMode.Impulse;

        // Start running animation on start
        playerAnimator.SetFloat("Speed_f", 1.0f);
    }

    void Update()
    {
        // Press space to jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, jumpForceMode);
            isOnGround = false;

            // Activating the animator trigger for jumping
            playerAnimator.SetTrigger("Jump_trig");

            // Stop dirt particles on jump
            dirtParticle.Stop();

            // Play jumpSound
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Let the player jump when they're back on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Play dirt particles when player is back on ground
            dirtParticle.Play();

            isOnGround = true;
        }

        // Make gameOver == true when player hits an obstacle
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            gameOver = true;

            // Play death animation
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

            // Play explosion particles
            explosionParticle.Play();

            // Play crash sound
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}