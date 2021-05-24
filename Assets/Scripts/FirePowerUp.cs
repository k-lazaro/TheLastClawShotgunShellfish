using UnityEngine;

public class FirePowerUp : HealthPowerUp
{
    public float increase;

    // Start is called before the first frame update
    void Start()
    {
        increase = 0.15f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            GameObject hero = collision.gameObject;
            Hero heroScript = hero.GetComponent<Hero>();

            if (heroScript)
            {
                heroScript.fireRate = Mathf.Max(heroScript.fireRate - increase, 0.1f);
                Destroy(gameObject);
            }
        }
    }
}
