using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy //inherit from the Enemy class
{
    private int _rotation; //create a private integer variable responsible for rotating the Enemy 45 degree

    
    void Start()
    {
        _rotation = Random.Range(0.0f, 1.0f) > 0.5f ? 45 : -45; //set _rotation to a random degree of 45 or -45
        transform.rotation = Quaternion.Euler(0, 0, _rotation); //apply the rotation onto the Enemy coordinates so when the game starts, it will already be rotated
    }
   
    public override void Move() //override the Move() method created in Enemy(). 
    {
        Vector3 tempPos = pos; //create a new Vector3 variables tempPos to track Enemy position
        tempPos.y -= speed * Time.deltaTime; //direction of this Enemy will always face the negative y-direction

        if(_rotation == 45) 
        {
            tempPos.x += speed * Time.deltaTime; //if the rotation is positive 45 degrees, then it should move in the positive x-direction
        }
        else
        {
            tempPos.x -= speed * Time.deltaTime; //if the rotation is negative 45 degrees, then it should move in the negative x-direction
        }
        pos = tempPos; //apply the change in position
    }
}
