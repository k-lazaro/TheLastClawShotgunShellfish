using System.Collections;          // Required for Arrays & other Collections
using System.Collections.Generic;  // Required for Lists and Dictionaries
using UnityEngine;                 // Required for Unity

public class Enemy : MonoBehaviour
{

    [Header("Set in Inspector: Enemy")]

    public float speed = 10f;      // The speed in m/s
    public float fireRate = 0.3f;  // Seconds/shot (Unused)
    public float health = 10;
    public int score = 100;      // Points earned for destroying this

    private BoundsCheck bndCheck;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.tag == "ProjectileHero")
        {
            Destroy(otherGameObject); // Destroy the Projectile
            Destroy(gameObject);      // Destroy this Enemy GameObject
        }
    }

    public virtual void Move()
    {

        Vector2 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime; //Time.deltaTime makes any timed based calculation independent from frame rate
        //tempPos.x -= 4 * Time.deltaTime; 
        pos = tempPos;

    }

}
