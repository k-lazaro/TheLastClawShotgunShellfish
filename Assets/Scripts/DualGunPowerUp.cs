using UnityEngine;

public class DualGunPowerUp : HealthPowerUp
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
                heroScript.twoGuns = true;
                Destroy(gameObject);
            }
        }
    }
}
