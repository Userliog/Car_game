using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchOn = false;
    float currentTime;
    public Text timeText;

    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        if (stopwatchOn == true)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.ToString(@"mm\:ss\:fff");
    }
    public void StartStopwatch()
    {
        stopwatchOn = true;
    }
    public void StopStopwatch()
    {
        stopwatchOn = false;
        print(timeText + " hjk "+ currentTime);
        //return (currentTime);
    }
}