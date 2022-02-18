/*
 * Zach Daly
 * Assignment 4
 * Controls player UI
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerX : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    public bool won = false;

    public PlayerControllerX playerControllerScript;

    void Start()
    {
        if (scoreText == null)
            scoreText = FindObjectOfType<Text>();
        if (playerControllerScript == null)
            playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerX>();

        scoreText.text = "Score: ";
    }

    void Update()
    {
        // Display score until gameOver == true
        if (!playerControllerScript.gameOver)
            scoreText.text = "Score: " + score;

        // Lose on bomb hit
        if (playerControllerScript.gameOver && !won)
            scoreText.text = "You Lose.\nPress (R) to Try Again!";

        if (score >= 5)
        {
            playerControllerScript.gameOver = true;
            won = true;
            scoreText.text = "You Won!!\nPress (R) to Try Again!";
        }

        // Restart
        if (playerControllerScript.gameOver && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}