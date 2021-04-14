using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urchin : EnemyProjectile
{
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    protected override IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.tag == "ProjectileHero")
        {
            Destroy(otherGameObject); // Destroy the Projectile
        }
        //Debug.Log("In Urchin");
        yield return null;
    }

}
