using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableobjectsChanger : MonoBehaviour
{
    [Header ("Scriptable Objects")]
    [SerializeField] private ScriptableObject[] ScriptableObjects;

    [Header("Display Scripts")]
    [SerializeField] private MapDisplay mapDisplay;
    [SerializeField] private CarDisplay carDisplay;
    private int currentIndex;

    private void Awake()
    {
        ChangeScriptableobject(0); 
    }
    private void ChangeScriptableobject(int change)
    {
        currentIndex += change;
        if (currentIndex < 0)
        {
            currentIndex = ScriptableObjects.Length - 1;
        }
        else if(currentIndex > ScriptableObjects.Length-1)
        {
            currentIndex = 0;
        }

        if (mapDisplay != null)
        {
            mapDisplay.DisplayMap((Map)ScriptableObjects[currentIndex]);
        }
        if (carDisplay != null)
        {
            carDisplay.DisplayCar((Car)ScriptableObjects[currentIndex]);
        }
    }
}
