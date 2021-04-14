using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static public Timer Instance;                                // A singleton for Timer

    public float timeStart;
    public Text textBox;
    public int difficulty;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            difficulty = 0;
            timeStart = 0;
            textBox.text = timeStart.ToString();
        }
        else Debug.LogError("Timer.Awake() - Attempted to assign second Timer instance");
    }

    // Update is called once per frame
    void Update()
    {
        if (Main.Instance.started)
        {
            timeStart += Time.deltaTime;

            // Sets difficulty based on time
            //if (timeStart > 15)
            //    difficulty = 1;
            //if (timeStart > 30)
            //    difficulty = 2;
            //if (timeStart > 50)
            //    difficulty = 3;
            //if (timeStart > 75)
            //    difficulty = 4;
            //if (timeStart > 90)
            //    difficulty = 5;

            if (timeStart > 15)
                difficulty = 1;
            if (timeStart > 30)
                difficulty = 2;
            if (timeStart > 50)
                difficulty = 3;
            if (timeStart > 75)
                difficulty = 4;
            if (timeStart > 85)
                difficulty = 5;

            textBox.text = Mathf.Round(timeStart).ToString();
        }
        
    }
}
