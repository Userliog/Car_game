using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private void Awake()
    {
        Transform Transform = transform.Find("Checkpoints");

        foreach (Transform N in Transform)
        {
            CheckpointCheck checkpoint = N.GetComponent<Checkpoint>();
            chechpoint.SetTrackCheckpoints(this);
        }
    }

    public void ThroughCheckpoint(Checkpointcheck checkpoint) {
        Debug.Log(checkpoint.transform.name);
    }
}
