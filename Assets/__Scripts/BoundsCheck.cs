using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")] //display in the Unity Inspector
    public float radius = 1f; //declare a public radius variable that will be applied to prevent any part of the player to go off-screen
    public bool keepOnScreen = true; 

    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool destroyRight, destroyLeft, destroyUp, destroyDown; //boolean flags to check if an enemy is destroyed, and where

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate() //called after every frame after Update() has been called on all GameObjects
    {
        Vector3 pos = transform.position; 
        isOnScreen = true;
        destroyRight = destroyLeft = destroyUp = destroyDown = false;

        if (pos.x > camWidth - radius) //if statement to check if an enemy is off the screen to the right
        {
            pos.x = camWidth - radius;
            isOnScreen = false;
            destroyRight = true;
        }
        if (pos.x < -camWidth + radius) //if statement to check if an enemy is off the screen to the left
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
            destroyLeft = true;

        }
        if (pos.y > camHeight - radius) //if statement to check if an enemy is off the screen at the top
        {
            pos.y = camHeight - radius;
            isOnScreen = false;
            destroyUp = true;
        }
        if (pos.y < -camHeight + radius) //if statement to check if an enemy is off the screen at the bottom
        {
            pos.y = -camHeight + radius;
            isOnScreen = false;
            destroyDown = true;
        }
        if(keepOnScreen && !isOnScreen) //if both these conditions return true, the enemy is on the screen and is not destroyed
        {
            transform.position = pos;
            isOnScreen = true;
            destroyRight = destroyLeft = destroyUp = destroyDown = false;
        }
        transform.position = pos;
    }

    void OnDrawGizmos() //draw the boundaries of the scene using OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
