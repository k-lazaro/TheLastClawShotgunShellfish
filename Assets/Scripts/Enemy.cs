using System.Collections;          // Required for Arrays & other Collections
using System.Collections.Generic;  // Required for Lists and Dictionaries
using UnityEngine;                 // Required for Unity

public class Enemy : MonoBehaviour
{

    [Header("Set in Inspector: Enemy")]

    public float speed = 15f;      // The speed in m/s
    //public float fireRate = 0.3f;  // Seconds/shot (Unused)
    public float health;
    public bool alive;
    //public int score = 100;      // Points earned for destroying this

    protected BoundsCheck bndCheck;
    public Animator animator;

    // This is a Property: A method that acts like a field
    public Vector2 pos
    {                                                     // a
        get
        {
            return (this.transform.position);
        }

        set
        {
            this.transform.position = value;
        }

    }

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        health = 1;
        alive = true;
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {

        Move();

        if (bndCheck != null && bndCheck.offDown)
        {
            // We're off the bottom, so destroy this GameObject
            Destroy(gameObject);
        }


    }

    protected virtual IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.tag == "ProjectileHero")
        {
            health--;
            Destroy(otherGameObject); // Destroy the Projectile
            if (health == 0)
            {
                alive = false;
                animator.SetTrigger("Dead");
                yield return new WaitForSeconds(0.917f);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                // Enemy drops powerup
                float rng = Random.Range(0.0f, 1.0f);
                GameObject powerup = null;
                // Drops health 20% of time
                if (rng <= 0.2f)
                {
                    // Health power up
                    powerup = Instantiate<GameObject>(PowerUpSpawn.Instance.spawnerPrefab[3]);
                }
                else if (rng >= 0.6f)
                {
                    // Fire rate power up
                    powerup = Instantiate<GameObject>(PowerUpSpawn.Instance.spawnerPrefab[2]);
                }
                if (powerup != null)
                {
                    powerup.transform.position = gameObject.transform.position;
                    // Wait four seconds until disappear
                    yield return new WaitForSeconds(4.0f);
                    Destroy(powerup);
                }
                Destroy(gameObject);      // Destroy this Enemy GameObject
            }
            else
                yield return null;
        }
    }

    public virtual void Move()
    {
        if (health != 0)
        {
            Vector2 tempPos = pos;
            tempPos.y -= speed * Time.deltaTime; //Time.deltaTime makes any timed based calculation independent from frame rate
            //tempPos.x -= 4 * Time.deltaTime;
            pos = tempPos;
        }
    }

}
