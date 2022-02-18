/*
 * Zach Daly
 * Assignment 4
 * Adds one to score upon money hit
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOneScoreX : MonoBehaviour
{
    private UIManagerX uIManager;
    private bool triggered = false;

    void Start()
    {
        uIManager = FindObjectOfType<UIManagerX>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            uIManager.score++;
        }
    }
}