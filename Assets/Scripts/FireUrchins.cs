﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUrchins : FireBubbles
{
    protected float spriteWidth;
    protected float spriteWidthHeight;
    [SerializeField]
    protected Vector2 startingPoint;
    public float horizontalCameraThreshold;

    void Awake()
    {
        spriteWidth = UrchinPool.urchinPoolInstance.GetBubble().GetComponent<SpriteRenderer>().bounds.size.x;
        horizontalCameraThreshold = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
    }

    protected override void Fire()
    {
        startingPoint = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0f, 4f) + spriteWidth, Screen.height));
        while (startingPoint.x < horizontalCameraThreshold)
        {
            float xStep = 0;
            if (Random.Range(0.0f, 1.0f) <= 0.5f)
            {
                xStep = Random.Range(3.0f, 6.0f);
            }
            else
            {
                xStep = Random.Range(1.0f, 3.0f);
            }
            
            Vector2 bubMoveVector = new Vector3(0, -1f);

            GameObject bub = UrchinPool.urchinPoolInstance.GetBubble();
            bub.transform.position = startingPoint;
            bub.GetComponent<EnemyProjectile>().SetActiveTime(7f);
            bub.SetActive(true);
            bub.GetComponent<EnemyProjectile>().SetMoveDirection(bubMoveVector);    // Polymorphism for component

            startingPoint.x += xStep;
        }

        //for (int i = 0; i < bubblesAmount + 1; i++)
        //{
            
        //    float xStep = Random.Range(3.0f, 6.0f);
        //    Vector2 bubMoveVector = new Vector3(0, -1f);

        //    GameObject bub = UrchinPool.urchinPoolInstance.GetBubble();
        //    bub.transform.position = startingPoint;
        //    bub.GetComponent<EnemyProjectile>().SetActiveTime(7f);
        //    bub.SetActive(true);
        //    bub.GetComponent<EnemyProjectile>().SetMoveDirection(bubMoveVector);    // Polymorphism for component

        //    startingPoint.x += xStep;
        //}
    }
}
