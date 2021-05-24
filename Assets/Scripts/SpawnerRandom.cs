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

    public float timeBetweenEnemySpawn = 1.5f; // # Enemies/second aka spawn rate
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
                        StartCoroutine(spawnRandomPositionSpawner(0, 2.0f));

                        fb = spawnerPrefab[1].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(.9f, 10);
                        StartCoroutine(spawnRandomPositionSpawner(1, 1.45f));

                        SpawnEnemy(Random.Range(3, 4));
                        break;
                    case 3:
                        // One Coral, One Bubble
                        fb.setRatioBool(false);
                        timeBetweenSpawn = 4.0f;
                        fb = spawnerPrefab[2].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(2.0f, 0);
                        StartCoroutine(spawnStaticPositionSpawner(2, Vector2.zero, 4.0f));

                        fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(.5f, 20);
                        StartCoroutine(spawnRandomPositionSpawner(0, 4.0f));
                        break;
                    case 4:
                        //fb.setRatioBool(false);
                        //timeBetweenSpawn = 8.0f;
                        //fb = spawnerPrefab[2].GetComponent<FireBubbles>();
                        //fb.setFireRateAmount(2.0f, 0);
                        //StartCoroutine(spawnStaticPositionSpawner(2, Vector2.zero, 4.0f));

                        // One Bubble
                        timeBetweenSpawn = 8.0f;
                        fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                        fb.setFireRateAmount(0.025f, 0);
                        fb.setAngleStep(10.134038f);
                        fb.setRatioBool(true);
                        StartCoroutine(spawnRandomPositionSpawner(0, 6.0f));
                        break;
                    case 5:
                        timeBetweenSpawn = 1.5f;
                        fb = spawnerPrefab[1].GetComponent<FireBubbles>();
                        fb.setRatioBool(false);
                        fb = spawnerPrefab[2].GetComponent<FireBubbles>();
                        fb.setRatioBool(false);
                        StartCoroutine(spawnRandomPositionSpawner(Random.Range(1, spawnerPrefab.Length), 0f));

                        float rng = Random.Range(0.0f, 1.0f);
                        if (rng < 0.20f)
                        {
                            fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                            fb.setFireRateAmount(0.025f, 0);
                            fb.setAngleStep(10.134038f);
                            fb.setRatioBool(true);
                            StartCoroutine(spawnRandomPositionSpawner(0, 2.0f));
                        }
                        else if (rng < 0.40f)
                        {
                            fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                            fb.setRatioBool(true);
                            fb.setFireRateAmount(0.035f, 0);
                            fb.setAngleStep(1.99999f);
                            StartCoroutine(spawnRandomPositionSpawner(0, 2.0f));
                        }
                        else if (rng < 0.60f)
                        {
                            fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                            fb.setRatioBool(true);
                            fb.setFireRateAmount(0.035f, 0);
                            fb.setAngleStep(2.53243783f);
                            StartCoroutine(spawnRandomPositionSpawner(0, 2.0f));
                        }
                        else if (rng < 0.75)
                        {
                            fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                            fb.setRatioBool(true);
                            fb.setFireRateAmount(0.05f, 0);
                            fb.setAngleStep(0.1430681f);
                            StartCoroutine(spawnRandomPositionSpawner(0, 2.0f));
                        }
                        else
                        {
                            fb = spawnerPrefab[0].GetComponent<FireBubbles>();
                            fb.setFireRateAmount(0.025f, 0);
                            fb.setAngleStep(10.166407f);
                            fb.setRatioBool(true);
                            StartCoroutine(spawnRandomPositionSpawner(0, 2.0f));
                        }
                        //fb.setAngleStep(10.134038f);
                        SpawnEnemy(Random.Range(3, 4));
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
                        // Swordfish
                        SpawnEnemy(0);
                        break;
                    case 2:
                        // Squid
                        timeBetweenEnemySpawn = 7.0f;
                        SpawnEnemy(1);
                        break;
                    case 3:
                        // Clam
                        timeBetweenEnemySpawn = 7.0f;
                        SpawnEnemy(Random.Range(3, 4));
                        SpawnEnemy(Random.Range(3, 4));
                        SpawnEnemy(2, new Vector2(Random.Range(-6.0f, 6.0f), Random.Range(7.0f, 12.0f)));
                        break;
                    case 4:
                        timeBetweenEnemySpawn = 7.0f;
                        SpawnRandomEnemy();
                        SpawnRandomEnemy();
                        SpawnEnemy(Random.Range(3, 4));
                        break;
                    case 5:
                        timeBetweenEnemySpawn = 5.5f;
                        SpawnRandomEnemy();
                        SpawnRandomEnemy();
                        SpawnRandomEnemy();
                        //float rng = Random.Range(0.0f, 1.0f);
                        // Spawn clam 15 percent of time
                        //if (rng <= 0.15f) 
                        //    SpawnEnemy(2, new Vector2(Random.Range(-6.0f, 6.0f), Random.Range(7.0f, 12.0f)));
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
        float distanceBetweenCrabVector = Vector2.Distance(randomVec, Hero.Instance.gameObject.transform.position);
        //Debug.Log(Vector2.Distance(randomVec, Hero.Instance.gameObject.transform.position));
        while (distanceBetweenCrabVector < 4.0)     // Prevent from spawning too close to player
        {
            //Debug.Log("Triggered movement");
            randomVec.x += 3.0f;
            distanceBetweenCrabVector = Vector2.Distance(randomVec, Hero.Instance.gameObject.transform.position);
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

        if (enemy.CompareTag("Trash"))
        {
            float yMin = -bndCheck.camHeight + (enemyPadding * 5);
            float yMax = bndCheck.camHeight/4 - enemyPadding;
            pos.y = Random.Range(yMin, yMax);
            float leftOrRightSide = Random.Range(0.0f, 1.0f);
            if (leftOrRightSide <= 0.5f)
            {
                // left
                pos.x = -bndCheck.camWidth - enemyPadding;
                enemy.GetComponent<Trash>().left = true;
            }
            else
            {
                pos.x = bndCheck.camWidth + enemyPadding;
                enemy.GetComponent<Trash>().left = false;
            }
        }
        else
        {
            float xMin = -bndCheck.camWidth + enemyPadding;
            float xMax = bndCheck.camWidth - enemyPadding;
            pos.x = Random.Range(xMin, xMax);
            pos.y = bndCheck.camHeight + enemyPadding;
        }

        enemy.transform.position = pos;
    }

    public void SpawnEnemy(int index, Vector2 pos)
    {
        GameObject enemy = Instantiate<GameObject>(prefabEnemies[index]);
        float distanceBetweenCrabVector = Vector2.Distance(pos, Hero.Instance.gameObject.transform.position);
        while (distanceBetweenCrabVector < 4.0)     // Prevents from spawning too close to player
        {
            //Debug.Log("Triggered movement");
            pos.x += 3.0f;
            distanceBetweenCrabVector = Vector2.Distance(pos, Hero.Instance.gameObject.transform.position);
        }
        enemy.transform.position = pos;
    }

    public void SpawnEnemy(int index)
    {
        //Debug.Log("Spawn enemy");
        GameObject enemy = Instantiate<GameObject>(prefabEnemies[index]);

        // Position the Enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;

        if (enemy.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(enemy.GetComponent<BoundsCheck>().radius);
        }

        // Set the initial position for the spawned Enemy
        Vector3 pos = Vector3.zero;

        if (enemy.CompareTag("Trash"))
        {
            float yMin = -bndCheck.camHeight + (enemyPadding * 5);
            float yMax = bndCheck.camHeight/4 - enemyPadding;
            pos.y = Random.Range(yMin, yMax);
            float leftOrRightSide = Random.Range(0.0f, 1.0f);
            if (leftOrRightSide <= 0.5f)
            {
                // left
                pos.x = -bndCheck.camWidth - enemyPadding;
                enemy.GetComponent<Trash>().left = true;
            }
            else
            {
                pos.x = bndCheck.camWidth + enemyPadding;
                enemy.GetComponent<Trash>().left = false;
            }      
        }
        else
        {
            float xMin = -bndCheck.camWidth + enemyPadding;
            float xMax = bndCheck.camWidth - enemyPadding;
            pos.x = Random.Range(xMin, xMax);
            pos.y = bndCheck.camHeight + enemyPadding;
        }

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