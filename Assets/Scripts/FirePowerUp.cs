﻿using UnityEngine;

public class FirePowerUp : HealthPowerUp
{
    public float increase = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            GameObject hero = collision.gameObject;
            Hero heroScript = hero.GetComponent<Hero>();

            if (heroScript)
            {
                heroScript.fireRate += increase;
                Destroy(gameObject);
            }
        }
    }
}
