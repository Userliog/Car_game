using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOptionsScript : MonoBehaviour
{
    [SerializeField] private Text TimeOfDayText;
    private int currentTimeIndex;

    public Transform lightContainer;
    public Light[] lightsArray;

    void Start()
    {
        //This takes all lights, in the transform, and puts them in an array
        lightsArray = lightContainer.GetComponentsInChildren<Light>();

        TimeChanger(1);
    }

    /// <param 
    /// name="change"> Int: The amount to change the time index by.
    /// </param>
    /// <summary>
    ///  This function is used by the buttons on screen to change the time of day
    /// </summary>
    public void TimeChanger(int change)
    {
        //The current light is dissabled
        lightsArray[currentTimeIndex].enabled = false;

        currentTimeIndex += change;
        //Here it makes sure the index doesn't exide the contents of the array
        if (currentTimeIndex < 0)
        {
            currentTimeIndex = lightsArray.Length - 1;
        }
        else if (currentTimeIndex > lightsArray.Length - 1)
        {
            currentTimeIndex = 0;
        }

        //The new light is enabled and the text changes
        lightsArray[currentTimeIndex].enabled = true;
        TimeOfDayText.text = lightsArray[currentTimeIndex].name;
    }
}