using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    [SerializeField] private GameObject TrackCheckpointsUI;
    [SerializeField] private ResultsDisplay ResultsDisplay;
    public event EventHandler OnCorrectCheckpoint;
    public event EventHandler OnWrongCheckpoint;
    private List<CheckpointCheck> checkpointList;
    private int laps;
    private int numberOfLaps = 0;
    private string raceType;
    private string trackType;

    //private List<string> driverList;
    private string driverThroughCheckpoint = "null";

    [SerializeField] private Transform checkpointsTransform;
    private int nextCheckpointIndex = 0;

    private void Awake()
    {
        raceType = PlayerPrefs.GetString("RaceType");
        trackType = PlayerPrefs.GetString("TrackType");
        laps = PlayerPrefs.GetInt("MapLaps");
        print(PlayerPrefs.GetInt("MapLaps"));
        checkpointList = new List<CheckpointCheck>();
        foreach (Transform checkpointTransform in checkpointsTransform)
        {
            CheckpointCheck checkpoint = checkpointTransform.GetComponent<CheckpointCheck>();
            checkpoint.SetTrackCheckpoints(this);
            checkpointList.Add(checkpoint);
            //driverList.Add("null");
        }
        TrackCheckpointsUI.SetActive(false);
    }

    public void ThroughCheckpoint(CheckpointCheck checkpoint)
    {
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            CheckpointCheck correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Hide();
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
            TrackCheckpointsUI.SetActive(false);
        }
        else
        {
            TrackCheckpointsUI.SetActive(true);

            CheckpointCheck correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Show();
        }


        if (trackType == "circuit" && nextCheckpointIndex == 1)
        {
            if (numberOfLaps < laps)
            {
                numberOfLaps++;
            }
            else
            {
                if (raceType == "timetrial")
                {
                    ResultsDisplay.DisplayResultsTimeTrial();
                }
                else
                {
                    ResultsDisplay.DisplayResultsRace(driverThroughCheckpoint);
                }
            }
        } else if (trackType == "sprint" && nextCheckpointIndex == 0)
        {
            if (raceType == "timetrial")
            {
                ResultsDisplay.DisplayResultsTimeTrial();
            }
            else
            {
                ResultsDisplay.DisplayResultsRace(driverThroughCheckpoint);
            }
        }
    }
    public void DriverThroughCheckpoint(CheckpointCheck checkpoint, string driver)
    {
        int checkpointNumber = checkpointList.IndexOf(checkpoint);
        if(trackType == "circuit" && numberOfLaps == laps && checkpointNumber == 0)
        {
            print("Last lap & checkpoint 1");
            if (driverThroughCheckpoint == "null")
            {
                print("Fist through the checkpoint");
                driverThroughCheckpoint = driver;
                print(driverThroughCheckpoint + " through checkpoint " + checkpointList[checkpointNumber].name);
            }
            else
            {
                print("Last through the checkpoint");
            }
        }
        else if (trackType == "timetrial" && checkpointNumber == checkpointList.Count - 1 && driverThroughCheckpoint == "null")
        {
            print("Fist through the checkpoint");
            driverThroughCheckpoint = driver;
            print(driverThroughCheckpoint + " through checkpoint " + checkpointList[checkpointNumber].name);
        }
    }
}
