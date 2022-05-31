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
        //This gets the mesh of the checkpoint and the TrackCheckpoints script and saves them as variables 
        meshRenderer = GetComponent<MeshRenderer>();
        trackCheckpoints = transform.parent.parent.gameObject.GetComponent<TrackCheckpoints>();

        //This hides mesh, and makes the checkpont invinsible
        Hide();
    }
    private void Start()
    {
        
    }

    /// <param 
    /// name="collider"> Collider: The collider that triggered this collider the script is attached to.
    /// </param>

    /// <summary>
    ///  This function is called when something triggers the collider, and if its the player or opponent, then the script calles a function in the "TrackCheckpoints" script.
    /// </summary>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //If its the player that passed through, then the "ThroughCheckpoint" funtion in "trackCheckpoints" is called to see if the player hit the right checkpoint, the function "DriverThroughCheckpoint" is also called to track who passes the last checkpoint.
            trackCheckpoints.ThroughCheckpoint(this);
            trackCheckpoints.DriverThroughCheckpoint(this, collider.gameObject.tag);
        }
        if (collider.gameObject.tag == "Opponent")
        {
            //If its the opponetnt that passes through then only the "DriverThroughCheckpoint" function is called.
            trackCheckpoints.DriverThroughCheckpoint(this, collider.gameObject.tag);
        }
    }

    /// <summary>
    ///  This function shows the mesh of the checkpoint.
    /// </summary>
    public void Show()
    {
        meshRenderer.enabled = true;
    }

    /// <summary>
    ///  This function hides the mesh of the checkpoint.
    /// </summary>
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
