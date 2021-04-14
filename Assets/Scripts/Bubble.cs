using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : EnemyProjectile
{
    public AudioSource audioSource;
    public AudioClip[] clips;

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
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)], Random.Range(.15f, .25f));
            animator.SetTrigger("Pop");
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), Hero.Instance.GetComponent<Collider2D>(), true);
            Destroy(otherGameObject); // Destroy the Projectile
            yield return new WaitForSeconds(0.417f);
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), Hero.Instance.GetComponent<Collider2D>(), false);
            Destroy();      // Destroy this Enemy GameObject
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject otherGameObject = collision.gameObject;

    //    if (otherGameObject.tag == "ProjectileHero")
    //    {
    //        Destroy(otherGameObject);
    //        StartCoroutine(DestroyTimer());
    //    }
    //}

    //private IEnumerator DestroyTimer()
    //{
    //    Physics2D.IgnoreLayerCollision(8, 10, true);
    //    animator.Play("Bubble_Pop");
    //    yield return new WaitForSeconds(0.417f);
    //    Destroy();
    //    Physics2D.IgnoreLayerCollision(8, 10, false);
    //}
}
