using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCoral : FireUrchins
{
    void Awake()
    {
        //spriteWidthHalf = CoralPool.coralPoolInstance.GetBubble().GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    protected override void Fire()
    {
        startingPoint = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0f, 4f), Screen.height));
        for (int i = 0; i < bubblesAmount + 1; i++)
        {
            float xStep = Random.Range(3.0f, 6.0f);
            Vector2 bubMoveVector = new Vector3(0, -1f);

            GameObject bub = CoralPool.coralPoolInstance.GetBubble();
            bub.GetComponent<Coral>().setAttach(false);
            bub.transform.position = startingPoint;
            bub.GetComponent<EnemyProjectile>().SetActiveTime(7f);
            bub.SetActive(true);
            bub.GetComponent<EnemyProjectile>().SetMoveDirection(bubMoveVector);    // Polymorphism for component

            startingPoint.x += xStep;
        }
    }
}
