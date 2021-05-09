using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunPowerUp : HealthPowerUp
{

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
                heroScript.twoGuns = false;
                heroScript.shotGun = true;
                heroScript.shotGunTime = 0;
                Destroy(gameObject);
            }
        }
    }
}
