using UnityEngine;

public class FirePowerUp : HealthPowerUp
{
    public float increase = 0.2f;

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
