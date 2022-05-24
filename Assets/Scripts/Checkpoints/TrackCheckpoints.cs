using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    [SerializeField] private TrackCheckpointsUI TrackCheckpointsUI;
    [SerializeField] private ResultsDisplay ResultsDisplay;
    public event EventHandler OnCorrectCheckpoint;
    public event EventHandler OnWrongCheckpoint;
    private List<CheckpointCheck> checkpointList;
    [SerializeField] private Transform checkpointsTransform;
    private int nextCheckpointIndex = 0;

    private void Awake()
    {
        checkpointList = new List<CheckpointCheck>();
        foreach (Transform checkpointTransform in checkpointsTransform)
        {
            CheckpointCheck checkpoint = checkpointTransform.GetComponent<CheckpointCheck>();
            checkpoint.SetTrackCheckpoints(this);
            checkpointList.Add(checkpoint);
            print(checkpoint);
        }
        TrackCheckpointsUI.Hide();
        print(checkpointList.Count);
    }

    public void ThroughCheckpoint(CheckpointCheck checkpoint)
    {
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            CheckpointCheck correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Hide();
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
            TrackCheckpointsUI.Hide();
        }
        else
        {
            TrackCheckpointsUI.Show();

            CheckpointCheck correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Show();
        }
        if(nextCheckpointIndex == 0)//checkpointList.Count - 1)
        {
            print("nextCheckpointIndex = lastcheckpoint");
            if (PlayerPrefs.GetString("MapType") == "timetrial")
            {
                ResultsDisplay.DisplayResultsTimeTrial();
            }else if(PlayerPrefs.GetString("MapType") == "vs")
            {
                ResultsDisplay.DisplayResultsRace();
            }
        }
    }
}
