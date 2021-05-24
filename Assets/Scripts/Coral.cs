using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coral : EnemyProjectile
{
    private bool attach;
    private int health;
    void Awake()
    {
        health = 2;
        //Physics2D.IgnoreLayerCollision(10, 11, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        attach = false;
        moveSpeed = 7f;
    }

    // Updates X/Y pos instead of Hero.cs
    void Update()
    {
        if (!attach)
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void TriggerDestroy()
    {
        Destroy();
    }

    public void setAttach(bool b)
    {
        attach = b;
    }

    protected override void Destroy()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(false);
    }

    protected override IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        // Once hit, ignore y from mouse and change y pos to Coral pos + constant?
        GameObject otherGameObject = collision.gameObject;

        //if (collision.collider.GetType() == typeof(BoxCollider2D))
        //{
        if (otherGameObject.CompareTag("ProjectileHero"))
        {
            if (health == 2)
            {
                health--;
                Destroy(otherGameObject);           // Causes MissingReferenceException: The Object of type GameObject has been destroyed but you are still trying to access it
                animator.SetTrigger("Damaged");
                yield return null;
            }
            else if (health == 0)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), Hero.Instance.GetComponent<Collider2D>(), true);
                Destroy(otherGameObject);
                animator.SetTrigger("Dead");
                yield return new WaitForSeconds(0.667f);
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), Hero.Instance.GetComponent<Collider2D>(), false);
                if (attach) setAttach(false);
                Destroy();
            }
            else
            {
                health--;
                Destroy(otherGameObject);
            }
        }
        else if (otherGameObject.tag == "Hero")
        {
            setAttach(true);
            transform.SetParent(otherGameObject.transform);
        }
        yield return null;
        //}
    }
}
