using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUrchins : FireBubbles
{
    protected float spriteWidthHalf;
    protected float spriteWidthHeight;
    [SerializeField]
    protected Vector2 startingPoint;

    void Awake()
    {
        spriteWidthHalf = UrchinPool.urchinPoolInstance.GetBubble().GetComponent<SpriteRenderer>().bounds.size.x/2;
    }

    protected void Fire()
    {
       startingPoint = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0f, 4f), Screen.height));
        for (int i = 0; i < bubblesAmount + 1; i++)
        {
            
            float xStep = Random.Range(3.0f, 6.0f);
            Vector2 bubMoveVector = new Vector3(0, -1f);

            GameObject bub = UrchinPool.urchinPoolInstance.GetBubble();
            bub.transform.position = startingPoint;
            bub.GetComponent<EnemyProjectile>().SetActiveTime(7f);
            bub.SetActive(true);
            bub.GetComponent<EnemyProjectile>().SetMoveDirection(bubMoveVector);    // Polymorphism for component

            startingPoint.x += xStep;
        }
    }
}
