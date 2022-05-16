using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ScriptableObject[] ScriptableObjects;

    [Header("Display Scripts")]
    [SerializeField] private MapDisplay mapDisplay;
    [SerializeField] private CarDisplay carDisplay;
    [SerializeField] private MapOptionsDisplay optionsDisplay;
    private int currentIndex;

    private void Awake()
    {
        ChangeScriptableObject(0);
    }

    /// <summary>
    ///  This class is used by the buttons on screen to change the scriptable object of either the mapDisplay or carDisplay script
    /// </summary>
    public void ChangeScriptableObject(int change)
    {
        currentIndex += change;
        if (currentIndex < 0)
        {
            currentIndex = ScriptableObjects.Length - 1;
        }
        else if (currentIndex > ScriptableObjects.Length - 1)
        {
            currentIndex = 0;
        }
        //Here it gets the scriptableObject based on the button clicks of the player

        if (mapDisplay != null)
        {
            mapDisplay.DisplayMap((Map)ScriptableObjects[currentIndex]);
        }
        if (optionsDisplay != null)
        {
            //PlayerPrefs.SetInt("CarToLoad", currentIndex);
            optionsDisplay.DisplayMapOptions((LevelOptions)ScriptableObjects[currentIndex]);
        }
        if (carDisplay != null)
        {
            PlayerPrefs.SetInt("CarToLoad", currentIndex);
            //this saves the current car as a PlayerPrefs, witch can be saved and loaded in a diffrent unity scene
            carDisplay.DisplayCar((Car)ScriptableObjects[currentIndex]);
        }
        //This part checks if one of the scripts are avalible, and if they are they send the current scriptableObject is sent to a class in that script.
    }
}