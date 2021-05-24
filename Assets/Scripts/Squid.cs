using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : Enemy
{
    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        health = 2;
        alive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.tag == "ProjectileHero")
        {
            health--;
            Destroy(otherGameObject); // Destroy the Projectile
            if (health == 0)
            {
                alive = false;
                animator.SetTrigger("Hurt");
                yield return new WaitForSeconds(0.5f);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                // Enemy drops powerup
                float rng = Random.Range(0.0f, 1.0f);
                GameObject powerup = null;
                // Drops health 20% of time
                if (rng <= 0.25f)
                {
                    powerup = Instantiate<GameObject>(PowerUpSpawn.Instance.spawnerPrefab[3]);
                }
                // Drops fire rate 30% of time
                else if (rng >= 0.7f)
                {
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
            yield return null;
        }
    }

    public override void Move()
    {
        if (health != 0)
        {
            pos = Vector2.MoveTowards(transform.position, Hero.Instance.transform.position, speed * Time.deltaTime);
        }
    }
}
