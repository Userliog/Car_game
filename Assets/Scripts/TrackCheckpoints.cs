using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    public event EventHandler OnCorrectCheckpoint;
    public event EventHandler OnWrongCheckpoint;
    private List<CheckpointCheck> checkpointList;
    private int nextCheckpointIndex;

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkpointList = new List<CheckpointCheck>();
        foreach (Transform checkpointTransform in checkpointsTransform)
        {
            Debug.Log(checkpointTransform);
            CheckpointCheck checkpoint = checkpointTransform.GetComponent<CheckpointCheck>();
            checkpoint.SetTrackCheckpoints(this);
            checkpointList.Add(checkpoint);
        }

        nextCheckpointIndex = 0;
    }

    public void ThroughCheckpoint(CheckpointCheck checkpoint)
    {
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            CheckpointCheck correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Hide();
            //Debug.Log("Correct");
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
            OnCorrectCheckpoint?.Invoke(this, EventArgs.Empty);

        }
        else
        {
            Debug.Log("Wrong");
            OnWrongCheckpoint?.Invoke(this, EventArgs.Empty);

            CheckpointCheck correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Show();
        }

    }
}
