using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clam : Enemy
{
    private float openDuration;
    private float closedDuration;

    public bool open;

    void Start()
    {
        StartCoroutine(closeMouth());
    }

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        open = false;
        health = 2;
        alive = true;
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator closeMouth()
    {
        open = false;
        animator.SetBool("Opened", false);
        closedDuration = Random.Range(2.0f, 6.0f);
        yield return new WaitForSeconds(closedDuration);
        StartCoroutine(openMouth());
    }

    IEnumerator openMouth()
    {
        animator.SetBool("Opened", true);
        yield return new WaitForSeconds(0.02f);     // "Invincible" opening frames of clam
        open = true;
        openDuration = Random.Range(2.0f, 4.0f);
        yield return new WaitForSeconds(openDuration);
        StartCoroutine(closeMouth());
    }

    protected override IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.tag == "ProjectileHero")
        {
            if (open)
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
                    GameObject powerup = Instantiate<GameObject>(PowerUpSpawn.Instance.spawnerPrefab[3]);
                    powerup.transform.position = gameObject.transform.position;
                    // Wait four seconds until disappear
                    yield return new WaitForSeconds(8.0f);
                    Destroy(powerup);
                    Destroy(gameObject);      // Destroy this Enemy GameObject
                }
            }
            else
            {
                Destroy(otherGameObject); // Destroy the Projectile
            }
            yield return null;
        }
    }
}
