using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    protected Vector2 moveDirection;
    protected float moveSpeed;
    public float activeTime = 5f;
    public Animator animator;

    protected void OnEnable()
    {
        Invoke("Destroy", activeTime);          // How long each projectile lasts
    }

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

    public void SetActiveTime(float t)
    {
        activeTime = t;
    }

    public void SetMoveSpeed(float s)
    {
        moveSpeed = s;
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    protected virtual IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogError("Must be overriden");
        yield return null;
        //Debug.Log("In EnemyProjectile");
    }

    protected void Destroy()
    {
        gameObject.SetActive(false);
    }

    protected void OnDisable()
    {
        CancelInvoke();
    }
}
