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
        }
        TrackCheckpointsUI.SetActive(false);
    }

    /// <param 
    ///name="checkpoint"> A Gameobject with a collider that the car passes through.
    ///</param>

    /// <summary>
    ///  This class is used to make sure the player passes through the colliders in the correct oder, and to display the results UI if the player finishes the race.
    /// </summary>
    public void ThroughCheckpoint(CheckpointCheck checkpoint)
    {
        //This checks if the transform the player passed through is the nex on in the list, and other wise it displays a "missed checkpoit" message.
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

        //This checks if the collider that the player passed through is the last one in the race, and if it is it displays the result display depending on witch type of race it was.
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
        }
        else if (trackType == "sprint" && nextCheckpointIndex == 0)
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
    /// <param 
    ///name="checkpoint"> A Gameobject with a collider that either the opponent or player passes through.
    ///</param>

    /// <summary>
    ///  This class is used to see who passes the finnish line first.
    /// </summary>
    public void DriverThroughCheckpoint(CheckpointCheck checkpoint, string driver)
    {
        //It gets the place of the checkpoint in the list
        int checkpointNumber = checkpointList.IndexOf(checkpoint);

        //If the collider is the last one in the race and if the "driverThroughCheckpoint" variable is unchanged then the driver through the check point is the fist one through the checkpoint.
        if (trackType == "circuit" && numberOfLaps == laps && checkpointNumber == 0)
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
