using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : Enemy
{
    void Awake()
    {
        health = 2;
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
