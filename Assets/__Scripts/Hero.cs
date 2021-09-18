using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;
    [Header("Set in Inspector")] //declare public variables for the Hero, including speed, rollMultiplied, pitchMultiplied

    public float speed;
    public float rollMult;
    public float pitchMult;
    public float gameRestartDelay = 2f; //declares the time needed for the game to restart after Hero is defeated
    private GameObject _lastTrigger = null; //create a private GameObject that references the last GameObject to trigger OnTrigger for the player

    private void Awake()
    {
     if (S == null)
        {
            S = this; //instantiate the Singleton 
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S1"); //show error if user tries to set singleton after it has already been set
        }
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal"); //receive information from input class
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = transform.position; //create a Vector that controls the position of the Hero
        pos.x += xAxis * speed * Time.deltaTime; //allow for horizontal movement
        pos.y += yAxis * speed * Time.deltaTime; //allow for vertical movement
        transform.position = pos; //set transform.position to the Vector3 movement of the Hero
        transform.rotation = Quaternion.Euler(yAxis* pitchMult, -xAxis * rollMult, 0); //provide rotation for the ship when it moves
        
    }

    void OnTriggerEnter(Collider other) //create an OnTriggerEnter to track when an Enemy comes in contact with the Hero
    {
        Transform rootT = other.gameObject.transform.root; //track the root GameObject that comes into contact with the Hero
        GameObject anyInstance = rootT.gameObject; //create a GameObject variable "anyInstance" that stores the root GameObject

        if (anyInstance == _lastTrigger) //ensure that it is not the same triggering instance as _lastTrigger
        {
            return;
        }
        _lastTrigger = anyInstance;

        if (anyInstance.tag == "Enemy") //if the GameObject that contacts the Hero has a tag of "Enemy"
        {
            //other.gameObject.SetActive(false); //
            Destroy(anyInstance); //Destroy the enemy that comes into contact with Hero
            Destroy(this.gameObject); //destroy the Hero
            Main.S.DelayedRestart(gameRestartDelay); //used the singleton to restart the game after a delay
        }

        else
        {
            print("Triggered by non-enemy: " + anyInstance.name); //print the non-enemy GameObject name that collides with the Hero
        }

    }
}
