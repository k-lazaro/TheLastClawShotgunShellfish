using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    public GameObject livesManager;
    public GameObject[] lives = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        livesManager = GameObject.Find("LivesManager");
        lives[0] = livesManager.transform.GetChild(0).gameObject;
        lives[1] = livesManager.transform.GetChild(1).gameObject;
        lives[2] = livesManager.transform.GetChild(2).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {

            GameObject hero = collision.gameObject;
            Hero heroScript = hero.GetComponent<Hero>();

            if (heroScript)
            {
                heroScript._lives++;

                if (heroScript._lives == 3)
                {
                    lives[2].SetActive(true);
                    lives[1].SetActive(true);
                    lives[0].SetActive(true);
                }
                else if (heroScript._lives ==2)
                {
                    lives[2].SetActive(false);
                    lives[1].SetActive(true);
                    lives[0].SetActive(true);
                }
                else if (heroScript._lives == 1)
                {
                    lives[2].SetActive(false);
                    lives[1].SetActive(false);
                    lives[0].SetActive(true);
                }
                else if (heroScript._lives == 0)
                {
                    lives[2].SetActive(false);
                    lives[1].SetActive(false);
                    lives[0].SetActive(false);
                }


                Destroy(gameObject);
            }
        }
    }
}
