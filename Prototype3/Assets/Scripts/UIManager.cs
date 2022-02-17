using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Attach to UIManager
public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    public bool won = false;

    //** Public means you set it in Unity
    public PlayerController playerControllerScript;

    void Start()
    {
        // If I have more than one text object, use findGameObjectWithTag()
        if (scoreText == null)
            scoreText = FindObjectOfType<Text>();

        if (playerControllerScript == null)
            playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        scoreText.text = "Score: 0";
    }

    void Update()
    {
        // Display score until gameOver == true
        if (!playerControllerScript.gameOver)
            scoreText.text = "Score: " + score;

        // Lose on SINGLE obstacle hit
        if (playerControllerScript.gameOver && !won)
            scoreText.text = "You Lose.\nPress (R) to Try Again!";

        // Win on 10 points
        if (score >= 10)
        {
            playerControllerScript.gameOver = true;
            won = true;

            //playerControllerScript.StopRunning();

            scoreText.text = "You Win!!\nPress (R) to Try Again!";
        }

        // Restart if user presses R
        if (playerControllerScript.gameOver && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
