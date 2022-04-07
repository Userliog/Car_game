using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCheck : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Has to be the handeling script attached to the car
        if (other.TryGetComponent<FWDHandeling>(out FWDHandeling player))
        {
            trackCheckpoints.ThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints Checkpoints)
    {
        this.trackCheckpoints = Checkpoints;
    }
    public void Show()
    {
        meshRenderer.enabled = true;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
