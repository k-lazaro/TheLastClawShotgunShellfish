using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of bubble-like objects,
/// attached to empty game object.
/// </summary>
public class BubblePool : MonoBehaviour
{
    public static BubblePool bulletPoolInstance;

    [SerializeField]
    private GameObject pooledBubble;
    private bool notEnoughBubblesInPool = true;
    private List<GameObject> bubbles;

    private void Awake()
    {
        if (bulletPoolInstance == null)
            bulletPoolInstance = this;
        else
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.Instance!");
    }

    // Start is called before the first frame update
    void Start()
    {
        bubbles = new List<GameObject>();
    }

    public GameObject GetBubble()
    {
        if (bubbles.Count > 0)
        {
            for (int i = 0; i < bubbles.Count; i++)
            {
                if (!bubbles[i].activeInHierarchy)
                    return bubbles[i];
            }
        }

        if (notEnoughBubblesInPool)
        {
            GameObject bub = Instantiate(pooledBubble);
            bub.SetActive(false);
            bubbles.Add(bub);
            return bub;

        }
        return null;
    }
}