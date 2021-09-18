using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S; //declare a Singleton for Main
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; //declare an GameObject array to store the enmies
    public float enemiesPerSecond = 0.5f; 
    public float defaultPadding = 1.5f;

    private BoundsCheck boundCheckInstance; //declare a private instance of the BoundsCheck class to be used in Main.cs

    void Awake()
    {
        S = this;
        boundCheckInstance = GetComponent<BoundsCheck>(); //Declare a variable that tracks the BoundsCheck component on this GameObject
        Invoke("SpawnEnemy", 1f / enemiesPerSecond); //Invoke SpawnEnemy() once every two seconds (assuming enemiesPerSecond is 2)
    }
    public void SpawnEnemy()
    {
        int randomNumber = Random.Range(0, prefabEnemies.Length); //choose a random enemy inside the array 
        GameObject instantiateObject = Instantiate<GameObject>(prefabEnemies[randomNumber]); //instantiate that random array using Instantiate

        float enemyPadding = defaultPadding;
        if(instantiateObject.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(instantiateObject.GetComponent<BoundsCheck>().radius);
        }

        Vector3 position = Vector3.zero; //Set initial position of the enemy, where xMinimum and xMaximum are used to define the x-position limits
        float xMinimum = -boundCheckInstance.camWidth + enemyPadding;
        float xMaximum = boundCheckInstance.camWidth - enemyPadding;
        position.x = Random.Range(xMinimum, xMaximum); //set a random x-position between xMinimum and xMaximum
        position.y = boundCheckInstance.camHeight + enemyPadding; //always instantiate the enemy at the top of the screen
        instantiateObject.transform.position = position;

        Invoke("SpawnEnemy", 1f / enemiesPerSecond); //Invoke SpawnEnemy() again to create another enemy on-screen
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay); //Invole the Restart() method and pass in the value of delay to specify the time it takes to transition
    }

    public void Restart() //when the function Restart() is called, then restart the scene "SpaceSHUMPGAME"
    {
        SceneManager.LoadScene("SpaceSHUMPGame");
    }
}
