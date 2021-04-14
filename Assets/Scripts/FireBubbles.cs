using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to Bubble Spawner Prefab
/// Defines behavior for firing bubbles (as well as subclasses)
/// Set amount of bubbles, fire rate, start/end angles
/// For rotating firing, set fractional turn
/// Ex: https://www.youtube.com/watch?v=sj8Sg8qnjOg
/// </summary>

public class FireBubbles : MonoBehaviour
{
    [SerializeField]
    protected int bubblesAmount = 10;
    [SerializeField]
    protected float fireRate;          // 1 per 2 seconds
    [SerializeField]
    protected float fractionalTurn = 1.618034f; // Golden Ratio
    [SerializeField]
    protected float bulletActiveTime;       // Sets duration of bullet before destroying
    [SerializeField]
    protected float moveSpeed;

    protected float angleStep;
    protected float currentAngle;
    protected bool clockwise = false;
    public bool ratio;
    [SerializeField]
    protected float startAngle = 90f, endAngle = 270f;

    //private Vector2 bubbleMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (!ratio)
            InvokeRepeating("Fire", 0f, fireRate);
        //currentAngle = startAngle;
        else
            InvokeRepeating("FireRatio", 0f, fireRate);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setFireRateAmount(float fr, int amt)
    {
        fireRate = fr;
        bubblesAmount = amt;
    }

    public void setBulletActiveTime(float t)
    {
        bulletActiveTime = t;
    }

    public void setMoveSpeed(float s)
    {
        moveSpeed = s;
    }

    public void setRatioBool(bool b)
    {
        ratio = b;
    }

    protected void Fire()
    {
        float angleStep = (endAngle - startAngle) / bubblesAmount;
        float angle = startAngle;

        for (int i = 0; i < bubblesAmount + 1; i++)
        {
            float bubDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bubDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bubMoveVector = new Vector3(bubDirX, bubDirY, 0f);
            Vector2 bubDir = (bubMoveVector - transform.position).normalized;

            GameObject bub = BubblePool.bulletPoolInstance.GetBubble();
            bub.transform.position = transform.position;
            bub.transform.rotation = transform.rotation;
            bub.SetActive(true);
            //bub.GetComponent<EnemyProjectile>().SetActiveTime(bulletActiveTime);
            bub.GetComponent<EnemyProjectile>().SetMoveDirection(bubDir);    // Polymorphism for component

            angle += angleStep;
        }
        
        
    }

    // Keeps shooting out bubbles until end of life
    protected void FireRatio()
    {
        float bubDirX, bubDirY;
        angleStep = fractionalTurn * 2 * Mathf.PI;
        currentAngle += angleStep;

        if (clockwise)
        {
            bubDirX = transform.position.x - Mathf.Sin(currentAngle);
            bubDirY = transform.position.y - Mathf.Cos(currentAngle);
        }
        else
        {
            bubDirX = transform.position.x + Mathf.Sin(currentAngle);
            bubDirY = transform.position.y + Mathf.Cos(currentAngle);
        }

        Vector3 bubMoveVector = new Vector3(bubDirX, bubDirY, 0f);
        Vector2 bubDir = (bubMoveVector - transform.position).normalized;

        GameObject bub = BubblePool.bulletPoolInstance.GetBubble();
        bub.transform.position = transform.position;
        //bub.transform.rotation = transform.rotation;
        bub.SetActive(true);
        bub.GetComponent<EnemyProjectile>().SetMoveDirection(bubDir);    // Polymorphism for component
        
    }


}
