using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Enemy
{
    public bool left;

    void Awake()
    {
        speed = Random.Range(12f, 15f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        health = 1;
        alive = true;
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {

        Move();

        if (bndCheck != null && (bndCheck.offLeft || bndCheck.offRight))
        {
            // We're off the bottom, so destroy this GameObject
            Destroy(gameObject);
        }


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
                animator.SetTrigger("Dead");
                yield return new WaitForSeconds(0.667f);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                // Enemy drops powerup
                float rng = Random.Range(0.0f, 1.0f);
                GameObject powerup = null;
                // Drops health 30% of time
                if (rng <= 0.30f)
                {
                    // Health power up
                    powerup = Instantiate<GameObject>(PowerUpSpawn.Instance.spawnerPrefab[3]);
                }
                // Drop fire rate 65% of time
                else if (rng <= 0.95f)
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

    public override void Move()
    {
        if (health != 0)
        {
            Vector2 tempPos = pos;
            if (left)
                tempPos.x += speed * Time.deltaTime; //Time.deltaTime makes any timed based calculation independent from frame rate
            else
                tempPos.x -= speed * Time.deltaTime;
            //tempPos.x -= 4 * Time.deltaTime;
            pos = tempPos;
        }
    }
}
