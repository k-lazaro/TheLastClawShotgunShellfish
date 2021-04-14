using System.Collections;           // Required for Arrays & other Collections
using System.Collections.Generic;   // Required to use Lists or Dictionaries
using UnityEngine;                  // Required for Unity
using UnityEngine.SceneManagement;  // For loading & reloading of scenes
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Main Instance;                                // A singleton for Main
    public bool started;
    public Text pressSpace;
    public Text endText;

    //[Header("Set in Inspector")]

    //public GameObject[] prefabEnemies;       // Array of Enemy prefabs. You can add different types here
    //public float enemySpawnPerSecond = 0.5f; // # Enemies/second aka spawn rate
    //public float enemyDefaultPadding = 1.5f; // Padding for position

    private BoundsCheck bndCheck;

    void Awake()
    {
        pressSpace.text = "Press Space\n\nTo Start";
        started = false;

        Cursor.visible = false;
        if (Instance == null) 
            Instance = this; // Set the Singleton
        else
            Debug.LogError("Main.Awake() - Attempted to assign second Main instance");

        // Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();

        // Invoke SpawnEnemy() once (in 2 seconds, based on default values)
        //Invoke("SpawnEnemy", 1f / enemySpawnPerSecond); 

    }


    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pressSpace.text = "";
            started = true;
        }
    }

    public void DelayedRestart(float delay)
    {
        // Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);

    }

    public void Restart()
    {
        // Reload _Scene_0 to restart the game
        SceneManager.LoadScene("Main");

    }


    //public void SpawnEnemy()
    //{
    //    // Pick a random Enemy prefab to instantiate

    //    int index = Random.Range(0, prefabEnemies.Length);
    //    GameObject enemy = Instantiate<GameObject>(prefabEnemies[index]);

    //    // Position the Enemy above the screen with a random x position
    //    float enemyPadding = enemyDefaultPadding;

    //    if (enemy.GetComponent<BoundsCheck>() != null)
    //    { 
    //        enemyPadding = Mathf.Abs(enemy.GetComponent<BoundsCheck>().radius);
    //    }

    //    // Set the initial position for the spawned Enemy 
    //    Vector3 pos = Vector3.zero;

    //    float xMin = -bndCheck.camWidth + enemyPadding;
    //    float xMax = bndCheck.camWidth - enemyPadding;

    //    pos.x = Random.Range(xMin, xMax);
    //    pos.y = bndCheck.camHeight + enemyPadding;

    //    enemy.transform.position = pos;

    //    // Call SpawnEnemy() again
    //    Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

    //}

}
