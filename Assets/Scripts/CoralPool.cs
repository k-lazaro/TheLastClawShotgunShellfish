using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralPool : BubblePool
{
    public static CoralPool coralPoolInstance;

    private void Awake()
    {
        if (coralPoolInstance == null)
            coralPoolInstance = this;
        else
            Debug.LogError("Coral.Awake() - Attempted to assign second Coral.Instance!");
    }
}
