using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to GameManager object in Main
/// Defines behavior for different difficulties,
/// places spawners in random locations according to
/// parameters.
/// Set spawner prefabs array in inspector,
/// time (in sec.) between each spawn and when it is active
/// </summary>

public class SpawnerRandom : MonoBehaviour
{
    public GameObject[] spawnerPrefab;            // Ideally array of spawners
    public GameObject[] prefabEnemies;       // Array of Enemy prefabs. You can add different types here
    public FireBubbles fb;
    public float timeBetweenSpawn;

    public float timeBetweenEnemySpawn = 2.0f; // # Enemies/second aka spawn rate
    public float enemyDefaultPadding = 1.5f; // Padding for position
    //public int index;
    private float elapsedTime;                    // Keeps track of elapsed time for spawners
    private float elapsedTimeForEnemy;
    private BoundsCheck bndCheck;

    void Awake()
    {
        // Set for difficulty 0
        //StartCoroutine("spawnSpawner");
        bndCheck = GetComponent<BoundsCheck>();
        fb = spawnerPrefab[0].GetComponent<FireBubbles>();
        fb.setFireRateAmount(2, 10);
        //fb.setFireRateAmount(.2f, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        elapsedTimeForEnemy = 0;
        //spawnerPrefab.transform.position = Vector2.zero;
        //gameObject.transform.position = Vector2.zero;
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
                        // One bubble spawner
                        fb.setRatioBool(false);
                        //StartCoroutine(spawnRandomPositionSpawner(2, 1.45f));
                        StartCoroutine(spawnRandomPositionSpawner(0, 1.45f));
                        //timeActive = 5f;
                        timeBetweenSpawn = 1.5f;
                        break;
                    //case 0: StartCoroutine(spawnRandomPositionSpawner(0)); break;
                    case 1:
                        // Two bubble spawners
                        fb.setFireRateAmount(.8f, 10);
                        StartCoroutine(spawnRandomPositionSpawner(0, 1.45f));
                        StartCoroutine(spawnRandomPositionSpawner(0, 1.45f));
                        break;
                    case 2:
                        // One bubble, one urchin
                        fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(.6f, 12);
                        StartCoroutine(spawnRandomPositionSpawner(0, 1.45f));

                        fb = spawnerPrefab[1].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(.9f, 10);
                        StartCoroutine(spawnRandomPositionSpawner(1, 1.45f));
                        break;
                    case 3:
                        // Two Bubbles, One Urchin
                        timeBetweenSpawn = 2.0f;
                        fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                        StartCoroutine(spawnRandomPositionSpawner(0, 2.45f));
                        StartCoroutine(spawnRandomPositionSpawner(0, 2.45f));
                        fb.setFireRateAmount(.65f, 14);

                        fb = spawnerPrefab[1].GetComponent<FireBubbles>();
                        StartCoroutine(spawnStaticPositionSpawner(1, Vector2.zero, 1.45f));
                        break;
                    case 4:
                        // One Coral, One Bubble
                        fb.setRatioBool(false);
                        timeBetweenSpawn = 8.0f;
                        fb = spawnerPrefab[2].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(2.0f, 0);
                        StartCoroutine(spawnStaticPositionSpawner(2, Vector2.zero, 4.0f));

                        fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(0.025f, 0);
                        fb.setAngleStep(10.134038f);
                        fb.setRatioBool(true);
                        StartCoroutine(spawnRandomPositionSpawner(0, 8.0f));
                        break;
                    case 5:
                        timeBetweenSpawn = 6.0f;
                        fb.setRatioBool(true);
                        fb.setFireRateAmount(0.017f, 0);
                        fb.setAngleStep(1.99999f);

                        StartCoroutine(spawnRandomPositionSpawner(0, 6.0f));
                        //fb.setAngleStep(10.134038f);
                        StartCoroutine(spawnRandomPositionSpawner(0, 6.0f));
                        break;
                    default: break;
                }
                //StartCoroutine("spawnSpawner");
                //StartCoroutine(spawnMultipleSpawners(2));
            }
            elapsedTime += Time.deltaTime;

            // Separate timer for enemy spawning
            if (elapsedTimeForEnemy > timeBetweenEnemySpawn)
            {
                elapsedTimeForEnemy = 0;
                switch (Timer.Instance.difficulty)       // Changes difficulty of game
                {
                    case 1:
                        SpawnEnemy(0);
                        break;
                }

            }
            elapsedTimeForEnemy += Time.deltaTime;
        }

    }

    public IEnumerator spawnRandomPositionSpawner(int index, float timeActive)
    {
        //Debug.Log("Spawn");
        GameObject s = Instantiate<GameObject>(spawnerPrefab[index]);
        Vector2 randomVec = generateRandomVector();
        //Debug.Log(Vector2.Distance(randomVec, Hero.Instance.gameObject.transform.position));
        if (Vector2.Distance(randomVec, Hero.Instance.gameObject.transform.position) < 4.0)
        {
            Debug.Log("Triggered movement");
            randomVec.y += 3.0f;
        }
        s.transform.position = randomVec;
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

    public void SpawnRandomEnemy()
    {
        // Pick a random Enemy prefab to instantiate

        int index = Random.Range(0, prefabEnemies.Length);
        GameObject enemy = Instantiate<GameObject>(prefabEnemies[index]);

        // Position the Enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;

        if (enemy.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(enemy.GetComponent<BoundsCheck>().radius);
        }

        // Set the initial position for the spawned Enemy
        Vector3 pos = Vector3.zero;

        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;

        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;

        enemy.transform.position = pos;
    }

    public void SpawnEnemy(int index, Vector2 pos)
    {
        GameObject enemy = Instantiate<GameObject>(prefabEnemies[index]);
        enemy.transform.position = pos;
    }

    public void SpawnEnemy(int index)
    {
        Debug.Log("Spawn enemy");
        GameObject enemy = Instantiate<GameObject>(prefabEnemies[index]);

        // Position the Enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;

        if (enemy.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(enemy.GetComponent<BoundsCheck>().radius);
        }

        // Set the initial position for the spawned Enemy
        Vector3 pos = Vector3.zero;

        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;

        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;

        enemy.transform.position = pos;
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