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

    void Start()
    {
        // Set ref variables to components
        rb = GetComponent<Rigidbody>();

        // Modify gravity
        if (Physics.gravity.y > -10)
            Physics.gravity *= gravityModifier;

        // Set the force mode
        jumpForceMode = ForceMode.Impulse;
    }

    void Update()
    {
        // Press space to jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, jumpForceMode);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Let the player jump when they're back on the ground
        if (collision.gameObject.CompareTag("Ground"))
            isOnGround = true;

        // Make gameOver == true when player hits an obstacle
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            Debug.Log("Game Over!");
            gameOver = true;
        }
    }
}