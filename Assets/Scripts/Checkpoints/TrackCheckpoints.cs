using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    [SerializeField] private TrackCheckpointsUI TrackCheckpointsUI;
    public event EventHandler OnCorrectCheckpoint;
    public event EventHandler OnWrongCheckpoint;
    private List<CheckpointCheck> checkpointList;
    [SerializeField] private Transform checkpointsTransform;
    private int nextCheckpointIndex =0;

    private void Awake()
    {
        //if error V
        //checkpointList = new List<CheckpointCheck>();
        foreach (Transform checkpointTransform in checkpointsTransform)
        {
            CheckpointCheck checkpoint = checkpointTransform.GetComponent<CheckpointCheck>();
            checkpoint.SetTrackCheckpoints(this);
            checkpointList.Add(checkpoint);
            print(checkpoint);
            print(checkpointsTransform);
        }
        TrackCheckpointsUI.Hide();
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

    }
}
