/*
 * Zach Daly
 * Assignment 4
 * Controls obstacle spawning
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to Spawn Manager
public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2, repeatRate = 2;

    //** Private means you set it in Start()
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Spawn obstacles while gameOver == true
    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }
}