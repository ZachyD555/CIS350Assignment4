using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to background object
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        // Save the starting position to a vector3
        startPos = transform.position;

        // Set the repeatWidth to half of the width of the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        // If the background is further left than repeatWidth, reset to startPos
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
