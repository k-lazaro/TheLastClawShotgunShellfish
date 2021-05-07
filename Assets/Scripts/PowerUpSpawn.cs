using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject[] spawnerPrefab;            // Ideally array of spawners

    public HealthPowerUp hpu;
    public float timeBetweenSpawn;
    //public int index;
    private float elapsedTime;                    // Keeps track of elapsed time

    void Awake()
    {
        // Set for difficulty 0
        //StartCoroutine("spawnSpawner");

        //fb = spawnerPrefab[0].GetComponent<FireBubbles>();
        //fb.setFireRateAmount(2, 10);
        //fb.setFireRateAmount(.2f, 1);
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
                        StartCoroutine(spawnRandomPositionSpawner(0, 1.45f));
                        //timeActive = 5f;
                        //timeBetweenSpawn = 5f;
                        break;
                    //case 0: StartCoroutine(spawnRandomPositionSpawner(0)); break;
                    case 1:
                        // Two bubble spawners
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));
                        break;
                    case 2:
                        // One bubble, one urchin
                        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));

                        hpu = spawnerPrefab[1].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(1, 10.0f));
                        break;
                    case 3:
                        // Two Bubbles, One Urchin
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));

                        hpu = spawnerPrefab[1].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnStaticPositionSpawner(1, Vector2.zero, 10.0f));
                        break;
                    case 4:
                        // One Urchin, One Bubble
                        timeBetweenSpawn = 8.0f;
                        hpu = spawnerPrefab[2].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnStaticPositionSpawner(1, Vector2.zero, 10.0f));

                        hpu = spawnerPrefab[0].GetComponent<HealthPowerUp>();
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));
                        break;
                    case 5:
                        timeBetweenSpawn = 6.0f;

                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));
                        //fb.setAngleStep(10.134038f);
                        StartCoroutine(spawnRandomPositionSpawner(0, 10.0f));
                        break;
                    default: break;
                }
                //StartCoroutine("spawnSpawner");
                //StartCoroutine(spawnMultipleSpawners(2));
            }
            elapsedTime += Time.deltaTime;
        }

    }

    public IEnumerator spawnRandomPositionSpawner(int index, float timeActive)
    {
        //Debug.Log("Spawn");
        GameObject s = Instantiate<GameObject>(spawnerPrefab[index]);
        s.transform.position = generateRandomVector();
        yield return new WaitForSeconds(timeActive);
        Destroy(s);
    }

    public IEnumerator spawnStaticPositionSpawner(int index, Vector2 pos, float timeActive)
    {
        GameObject s = Instantiate<GameObject>(spawnerPrefab[index]);
        s.transform.position = pos;
        yield return new WaitForSeconds(timeActive);
        Destroy(s);
    }

    //public IEnumerator spawnMultipleSpawners(int num, int index)
    //{
    //    GameObject[] spawners = new GameObject[num];
    //    for (int i = 0; i < num; i++)
    //    {
    //        spawners[i] = Instantiate<GameObject>(spawnerPrefab[index]);
    //        spawners[i].transform.position = generateRandomVector();
    //    }
    //    yield return new WaitForSeconds(timeActive);
    //    for (int j = 0; j < num; j++)
    //    {
    //        Destroy(spawners[j]);
    //    }
    //}

    Vector2 generateRandomVector()
    {
        // Hard coded values
        return new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-1.5f, 13.5f));
    }
}
