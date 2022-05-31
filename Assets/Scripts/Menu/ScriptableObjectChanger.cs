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
    private int currentIndex;

    private void Awake()
    {
        //This cycles through the "ChangeScriptableObject" function to display the fist map/car.
        ChangeScriptableObject(0);
    }

    /// <param 
    /// name="change"> Int: The amount to change the scriptableObjects index by.
    /// </param>
    /// <summary>
    ///  This function is used by the buttons on screen to change the scriptable object of either the mapDisplay or carDisplay script
    /// </summary>
    public void ChangeScriptableObject(int change)
    {
        //Here it gets the scriptableObject based on the button clicks of the player, and makes sure the index doesn't exide the contents of the array.
        currentIndex += change;
        if (currentIndex < 0)
        {
            currentIndex = ScriptableObjects.Length - 1;
        }
        else if (currentIndex > ScriptableObjects.Length - 1)
        {
            currentIndex = 0;
        }
        

        //This part checks if the scripts are avalible, and if they are they send the current scriptableObject to a function in that script.
        if (mapDisplay != null)
        {
            mapDisplay.DisplayMap((Map)ScriptableObjects[currentIndex]);
        }

        if (carDisplay != null)
        {
            //this saves the current car as a PlayerPrefs, witch can be saved and loaded in a diffrent unity scene
            PlayerPrefs.SetInt("CarToLoad", currentIndex);
            carDisplay.DisplayCar((Car)ScriptableObjects[currentIndex]);
        }
        
    }
}