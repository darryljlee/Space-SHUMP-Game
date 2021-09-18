using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    private BoundsCheck boundCheckInstance;
    void Awake()
    {
        boundCheckInstance = GetComponent<BoundsCheck>();
    }

    public Vector3 pos{ //declare a property pos where you can get and set the value of pos, as if it is a class variable of Enemy
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Update()
    {
        Move(); //call the Move() function every second

        if(boundCheckInstance != null && (boundCheckInstance.destroyDown || boundCheckInstance.destroyRight || boundCheckInstance.destroyLeft))
        {//using the boundCheckInstance, call the BoundsCheck class to see if the Enemy is off the screen, allowing it be destroyed
            Destroy(gameObject);
        }
      
    }

    public virtual void Move() 
    {
        Vector3 tempPos = pos; //receives the current position of the enemy 
        tempPos.y -= speed * Time.deltaTime; //always direct Enemy1 to the bottom of the screen 
        pos = tempPos; //assign the Vector3 value to pos, setting Enemy1's position
    }
}