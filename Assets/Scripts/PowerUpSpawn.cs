using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : SpawnerRandom
{
    public static PowerUpSpawn Instance;

    public HealthPowerUp hpu;
    //public int index;
    private float elapsedTime;                    // Keeps track of elapsed time

    void Awake()
    {
        if (Instance == null)
            Instance = this; // Set the Singleton
        else
            Debug.LogError("PowerUpSpawn.Awake() - Attempted to assign second PowerUpSpawn instance");
    }

    // Start is called before the first frame update
    void Start()
    {
        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If space is pressed
        if (Main.Instance.started)
        {
            if (elapsedTime > timeBetweenSpawn)
            {
                elapsedTime = 0;
                switch (Timer.Instance.difficulty)       // Changes difficulty of game 
                {
                    case 0:
                        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(0, 1.45f));
                        //timeActive = 5f;
                        //timeBetweenSpawn = 5f;
                        break;
                    //case 0: StartCoroutine(spawnRandomPositionSpawner(0)); break;
                    case 1:
                        // Dual gun, shotgun
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));

                        hpu = spawnerPrefab[1].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(1, 10.0f));
                        break;
                    case 2:
                        // Dual gun, shotgun
                        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(1, 10.0f));

                        hpu = spawnerPrefab[1].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(1, 10.0f));
                        break;
                    case 3:
                        //  Dual gun, shotgun
                        StartCoroutine(spawnRandomPositionSpawner(1, 10.0f));
                        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));

                        hpu = spawnerPrefab[1].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnStaticPositionSpawner(1, Vector2.zero, 10.0f));
                        break;
                    case 4:
                        // Shotgun, Dual gun
                        timeBetweenSpawn = 8.0f;
                        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnStaticPositionSpawner(0, Vector2.zero, 10.0f));

                        hpu = spawnerPrefab[1].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(1, 10.0f));
                        break;
                    case 5:
                        // Dual gun, Shotgun
                        timeBetweenSpawn = 6.0f;
                        hpu = spawnerPrefab[1].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(1, 10.0f));
                        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));
                        break;
                    default: break;
                }
            }
            elapsedTime += Time.deltaTime;
        }

    }
}
