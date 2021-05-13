using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : MonoBehaviour
{
    static public Hero Instance; //This can be access anywhere by typing 'Hero.Instance'. Look up Singleton pattern to learn more.
    private Vector3 lastPosition;

    [Header("Set in Inspector")]
    public AudioSource audioSource;
    public AudioClip[] clips;

    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 4f;
    public GameObject projectilePrefab;
    //public GameObject shield;
    public float projectileSpeed = 40;

    // animator
    public Animator animator;

    // sprite
    public SpriteRenderer mySprite;

    // lives
    public GameObject[] lives;

    // invulnerability
    public float invulnerabiltyDuration;
    public int numberOfFlashes;
    //public float flashDuration;

    // determine fire rate
    private float nextFire = 0f;

    //Freeze y
    //public bool freezeY;

    public bool twoGuns;
    public float dualGunTime;
    public float dualGunLimit = 15;
    public GameObject PowerupManager;
    public GameObject[] powerup = new GameObject[2];


    public bool shotGun;
    public float shotGunTime;
    public float shotGunLimit = 15;


    [Header("Set Dynamically")]

    [SerializeField]
    public float fireRate = 1f;
    public float _lives = 3;
    private float elapsedTime = 0;
    //private float _shieldLevel = 1;
    //private GameObject lastTriggerGameObject = null;

    public float Lives
    {
        get
        {
            return (_lives);
        }

        set
        {
            _lives = Mathf.Min(value, 3);

            switch (value)
            {
                case 3:
                    lives[2].SetActive(true);
                    lives[1].SetActive(true);
                    lives[0].SetActive(true);
                    break;
                case 2:
                    lives[2].SetActive(false);
                    lives[1].SetActive(true);
                    lives[0].SetActive(true);
                    break;
                case 1:
                    lives[2].SetActive(false);
                    lives[1].SetActive(false);
                    lives[0].SetActive(true);
                    break;
                case 0:
                    lives[2].SetActive(false);
                    lives[1].SetActive(false);
                    lives[0].SetActive(false);
                    break;
                default:
                    break;
            }

            // If the shield is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);

                Main.Instance.endText.text = "You survived for\n\n" + Mathf.Round(Timer.Instance.timeStart) + " seconds!\n\n\n" +
                    "Restarting...";

                // Tell Main.S to restart the game after a delay
                Main.Instance.DelayedRestart(gameRestartDelay);
            }
        }

    }

    // Called at the start of the game, before Start()
    void Awake()
    {
        if (Instance == null)
        {
            _lives = 3;
            dualGunTime = 0;
            twoGuns = false;
            shotGunTime = 0;
            shotGun = false;
            PowerupManager = GameObject.Find("PowerupManager");
            powerup[0] = PowerupManager.transform.GetChild(0).gameObject;
            powerup[1] = PowerupManager.transform.GetChild(1).gameObject;
            Physics2D.IgnoreLayerCollision(8, 10, false);
            Instance = this; // Set the Singleton
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.Instance!");
        }



    }

    // Called every frame. This is the main loop for Hero
    void Update()
    {

        // Get player input

        //float xAxis = Input.GetAxis("Horizontal");
        //float yAxis = Input.GetAxis("Vertical");

        // Use that input to change transform.position

        //Vector2 pos = transform.position;
        //pos.x += xAxis * speed * Time.deltaTime;
        //pos.y += yAxis * speed * Time.deltaTime;
        if (Main.Instance.started)
        {
            // Player movement
            Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = targetPos;
            determineDirection();
            lastPosition = transform.position;

            // else
            // {
            //     // NOTE TO SELF: Coral drags down and prevents movement until mouse movement down is detected?
            //     Vector2 targetPos = lastPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //     transform.position += new Vector3(targetPos.x, 0);
            //     determineDirection();
            //     lastPosition = transform.position;
            // }


            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Fire();
            }


            if (dualGunTime > dualGunLimit)
            {
                dualGunTime = 0;
                powerup[1].SetActive(false);
                twoGuns = !twoGuns;
            }

            if (twoGuns)
            {
                dualGunTime += Time.deltaTime;
            }

            if (shotGunTime > shotGunLimit)
            {
               shotGunTime = 0;
               powerup[0].SetActive(false);
               shotGun = !shotGun;
            }

            if (shotGun)
            {
                shotGunTime += Time.deltaTime;
            }

        }


        // Rotate the ship to make it feel more dynamic
        // Feel free to remove this if you don't like the affect
        //transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Automatic shooting



    }

    void Fire()
    {

        audioSource.PlayOneShot(clips[0], 0.5f);


        if (shotGun)
        {
            Vector3 vec3 = new Vector3(.5f, 0, 0);
            Vector3 vec4 = new Vector3(-.5f, 0, 0);

            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = (Vector2)transform.position + Vector2.up;
            Rigidbody2D rigidB = projGO.GetComponent<Rigidbody2D>();
            rigidB.velocity = Vector2.up * projectileSpeed;

            Vector3 spread = new Vector3(0, 0, 5);
            GameObject bullet = Instantiate(projectilePrefab, transform.position + vec4, Quaternion.Euler(transform.rotation.eulerAngles + spread));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * projectileSpeed, ForceMode2D.Impulse);

            Vector3 spread2 = new Vector3(0, 0, -5);
            GameObject bullet2 = Instantiate(projectilePrefab, transform.position + vec3, Quaternion.Euler(transform.rotation.eulerAngles + spread2));
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(bullet2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        else if (twoGuns)
        {
            Vector2 vec2 = new Vector2(.5f,0);

            GameObject projGO2 = Instantiate<GameObject>(projectilePrefab);
            projGO2.transform.position = (Vector2)transform.position + vec2 + Vector2.up;
            Rigidbody2D rigidB2 = projGO2.GetComponent<Rigidbody2D>();
            rigidB2.velocity = Vector2.up * projectileSpeed;

            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = (Vector2)transform.position - vec2 + Vector2.up;
            Rigidbody2D rigidB = projGO.GetComponent<Rigidbody2D>();
            rigidB.velocity = Vector2.up * projectileSpeed;
        }
        else
        {
            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = (Vector2)transform.position + Vector2.up;
            Rigidbody2D rigidB = projGO.GetComponent<Rigidbody2D>();
            rigidB.velocity = Vector2.up * projectileSpeed;
        }

    }

    //private IEnumerator flash()
    //{
    //    int temp = 0;
    //    while (temp < numberOfFlashes)
    //    {

    //    }
    //}

    void determineDirection()
    {
        Vector2 currentDirection = (transform.position - lastPosition).normalized;
        if (shotGun)
        {
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkRight", false);
            animator.SetBool("Idle", false);
            animator.SetBool("DualgunWalkLeft", false);
            animator.SetBool("DualgunWalkRight", false);
            animator.SetBool("DualgunIdle", false);
            if (currentDirection.x == -1)
            {
                elapsedTime = 0;
                animator.SetBool("ShotgunWalkLeft", true);
                animator.SetBool("ShotgunWalkRight", false);
                animator.SetBool("ShotgunIdle", false);
                //yield return new WaitForSeconds(1f);
            }
            else if (currentDirection.x == 1)
            {
                elapsedTime = 0;
                animator.SetBool("ShotgunWalkLeft", false);
                animator.SetBool("ShotgunWalkRight", true);
                animator.SetBool("ShotgunIdle", false);
            }
            else
            {
                if (elapsedTime > 1)
                {
                    animator.SetBool("ShotgunWalkLeft", false);
                    animator.SetBool("ShotgunWalkRight", false);
                    animator.SetBool("ShotgunIdle", true);
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
            }
        }
        else if (twoGuns)
        {
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkRight", false);
            animator.SetBool("Idle", false);
            animator.SetBool("ShotgunWalkLeft", false);
            animator.SetBool("ShotgunWalkRight", false);
            animator.SetBool("ShotgunIdle", false);
            if (currentDirection.x == -1)
            {
                elapsedTime = 0;
                animator.SetBool("DualgunWalkLeft", true);
                animator.SetBool("DualgunWalkRight", false);
                animator.SetBool("DualgunIdle", false);
                //yield return new WaitForSeconds(1f);
            }
            else if (currentDirection.x == 1)
            {
                elapsedTime = 0;
                animator.SetBool("DualgunWalkLeft", false);
                animator.SetBool("DualgunWalkRight", true);
                animator.SetBool("DualgunIdle", false);
            }
            else
            {
                if (elapsedTime > 1)
                {
                    animator.SetBool("DualgunWalkLeft", false);
                    animator.SetBool("DualgunWalkRight", false);
                    animator.SetBool("DualgunIdle", true);
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
            }
        }
        else
        {
            animator.SetBool("ShotgunWalkLeft", false);
            animator.SetBool("ShotgunWalkRight", false);
            animator.SetBool("ShotgunIdle", false);
            animator.SetBool("DualgunWalkLeft", false);
            animator.SetBool("DualgunWalkRight", false);
            animator.SetBool("DualgunIdle", false);
            if (currentDirection.x == -1)
            {
                elapsedTime = 0;
                animator.SetBool("WalkLeft", true);
                animator.SetBool("WalkRight", false);
                animator.SetBool("Idle", false);
                //yield return new WaitForSeconds(1f);
            }
            else if (currentDirection.x == 1)
            {
                elapsedTime = 0;
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", true);
                animator.SetBool("Idle", false);
            }
            else
            {
                if (elapsedTime > 1)
                {
                    animator.SetBool("WalkLeft", false);
                    animator.SetBool("WalkRight", false);
                    animator.SetBool("Idle", true);
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
            }
        }
        //Debug.Log(currentDirection.x);
    }


    IEnumerator getInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
        //mySprite.color = Color.red;
        yield return new WaitForSeconds(invulnerabiltyDuration);
        //mySprite.color = Color.white;
        Physics2D.IgnoreLayerCollision(8, 10, false);
        Physics2D.IgnoreLayerCollision(10, 11, false);

        //int temp = 0;
        //float flashDuration = invulnerabiltyDuration /(2 * numberOfFlashes);
        //while (temp < numberOfFlashes)
        //{
        //    Physics2D.IgnoreLayerCollision(8, 10, true);
        //    mySprite.color = Color.red;
        //    yield return new WaitForSeconds(flashDuration);
        //    mySprite.color = Color.white;
        //    yield return new WaitForSeconds(flashDuration);
        //    Physics2D.IgnoreLayerCollision(8, 10, false);
        //    temp++;
        //}
    }

    /*
     * This function gets called everytime something hits this object
     * In this case, the Hero collider is a trigger, meaning objects can pass through.
     * Some colliders can be normal colliders, meaning objects can't pass through
     * See the circleCollider2D on this inspector, look for IsTrigger.
     */
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject gameObject = collision.gameObject;

    //    // Coral
    //    if (gameObject.tag == "Coral")
    //    {
    //        //Lives--;
    //        //animator.SetTrigger("Hurt");
    //        animator.SetBool("WalkLeft", false);
    //        animator.SetBool("WalkRight", false);
    //        animator.SetBool("Idle", false);
    //        StartCoroutine("getInvulnerable");
    //        // If the shield was triggered by an enemy
    //        //ShieldLevel--;        // Decrease the level of the shield by 1
    //        //if(ShieldLevel == 0)  // If no shield, hit it
    //        //{
    //        //    shield.SetActive(false);
    //        //}
    //        //Destroy(gameObject);          // … and Destroy the enemy
    //    }
    //    // Bubble or Urchin
    //    else if (gameObject.tag == "EnemyProj")
    //    {
    //        Lives--;
    //        animator.SetTrigger("Hurt");
    //        animator.SetBool("WalkLeft", false);
    //        animator.SetBool("WalkRight", false);
    //        animator.SetBool("Idle", false);
    //        StartCoroutine("getInvulnerable");
    //        //gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        print("Triggered by non-Enemy: " + gameObject.name);
    //    }
    //}

    // Used when object is not a trigger
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;

        // Coral
        if (gameObject.tag == "Coral")
        {
            //Lives--;
            //animator.SetTrigger("Hurt");
            //freezeY = true;
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkRight", false);
            animator.SetBool("Idle", false);
            //StartCoroutine("getInvulnerable");
            // If the shield was triggered by an enemy
            //ShieldLevel--;        // Decrease the level of the shield by 1
            //if(ShieldLevel == 0)  // If no shield, hit it
            //{
            //    shield.SetActive(false);
            //}
            //Destroy(gameObject);          // … and Destroy the enemy
        }
        // Bubble or Urchin
        else if (gameObject.tag == "EnemyProj" || (gameObject.tag == "Enemy" && gameObject.GetComponent<Enemy>().alive))
        {
            if (shotGun)
            {
                Lives--;
                animator.SetTrigger("ShotgunHurt");
                animator.SetBool("ShotgunWalkLeft", false);
                animator.SetBool("ShotgunWalkRight", false);
                animator.SetBool("ShotgunIdle", false);
                StartCoroutine("getInvulnerable");
                //gameObject.SetActive(false);
            }
            else if (twoGuns)
            {
                Lives--;
                animator.SetTrigger("DualgunHurt");
                animator.SetBool("DualgunWalkLeft", false);
                animator.SetBool("DualgunWalkRight", false);
                animator.SetBool("DualgunIdle", false);
                StartCoroutine("getInvulnerable");
                //gameObject.SetActive(false);
            }
            else
            {
                Lives--;
                animator.SetTrigger("Hurt");
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", false);
                animator.SetBool("Idle", false);
                StartCoroutine("getInvulnerable");
                //gameObject.SetActive(false);
            }
        }
        else
        {
            print("Triggered by non-Enemy: " + gameObject.name);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

}
