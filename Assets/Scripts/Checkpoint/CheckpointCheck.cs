using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointCheck : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        trackCheckpoints = transform.parent.parent.gameObject.GetComponent<TrackCheckpoints>();
    }
    private void Start()
    {
        Hide();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            trackCheckpoints.DriverThroughCheckpoint(this, collider.gameObject.tag);
            trackCheckpoints.ThroughCheckpoint(this);
        }
        if (collider.gameObject.tag == "Opponent")
        {
            trackCheckpoints.DriverThroughCheckpoint(this, collider.gameObject.tag);
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
