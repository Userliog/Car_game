using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Has to be a script attached to the car
        if (other.TryGetComponent<FWDHandeling>(out FWDHandeling player))
        {
            Debug.Log("1!");
        }
    }
}
