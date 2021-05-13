using UnityEngine;

public class DualGunPowerUp : HealthPowerUp
{

    public GameObject PowerupManager;
    public GameObject[] powerup = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        PowerupManager = GameObject.Find("PowerupManager");
        powerup[0] = PowerupManager.transform.GetChild(0).gameObject;
        powerup[1] = PowerupManager.transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            GameObject hero = collision.gameObject;
            Hero heroScript = hero.GetComponent<Hero>();

            if (heroScript)
            {
                heroScript.shotGun = false;
                heroScript.twoGuns = true;
                powerup[0].SetActive(false);
                powerup[1].SetActive(true);
                heroScript.dualGunTime = 0;
                Destroy(gameObject);
            }
        }
    }
}
