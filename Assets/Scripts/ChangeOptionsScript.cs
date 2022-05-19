using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOptionsScript : MonoBehaviour
{
    [SerializeField] private Text TimeOfDayText;
    private int oldTimeIndex;
    private int currentTimeIndex;

    public Transform lightContainer;
    public Light[] lightsArray;

    void Start()
    {
        lightsArray = lightContainer.GetComponentsInChildren<Light>();
        TimeChanger(1);
    }

    public void TimeChanger(int change)
    {
        currentTimeIndex += change;
        if (currentTimeIndex >= 0 && currentTimeIndex < lightsArray.Length)
        {
            lightsArray[oldTimeIndex].enabled = false;
            lightsArray[currentTimeIndex].enabled = true;
            oldTimeIndex = currentTimeIndex;
            if (currentTimeIndex == 0)
            {
                currentTimeIndex = lightsArray.Length;
            }
        }
        else if (currentTimeIndex > lightsArray.Length - 1)
        {
            currentTimeIndex = 0;
            lightsArray[oldTimeIndex].enabled = false;
            lightsArray[currentTimeIndex].enabled = true;
            oldTimeIndex = currentTimeIndex;
        }
        else if (currentTimeIndex < 0)
        {
            currentTimeIndex = lightsArray.Length;
            lightsArray[oldTimeIndex].enabled = false;
            lightsArray[currentTimeIndex].enabled = true;
            oldTimeIndex = currentTimeIndex;
        }
        TimeOfDayText.text = lightsArray[oldTimeIndex].name;
    }
}