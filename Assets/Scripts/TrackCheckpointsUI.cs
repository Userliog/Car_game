using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpointsUI : MonoBehaviour
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;

    private void Start()
    {
        trackCheckpoints.OnCorrectCheckpoint += TrackCheckpoints_OnCorrectCheckpoint;
        trackCheckpoints.OnWrongCheckpoint += TrackCheckpoints_OnWrongCheckpoint;
    }
    private void TrackCheckpoints_OnCorrectCheckpoint(object sender, System.EventArgs e)
    {

    }
    private void TrackCheckpoints_OnWrongCheckpoint(object sender, System.EventArgs e)
    {

    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
