using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrchinPool : BubblePool
{
    public static UrchinPool urchinPoolInstance;

    private void Awake()
    {
        if (urchinPoolInstance == null)
            urchinPoolInstance = this;
        else
            Debug.LogError("Urchin.Awake() - Attempted to assign second Urchin.Instance!");
    }
}
