using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text mapName;
    [SerializeField] private Text mapdescription;
    [SerializeField] private Image mapImage;
    [SerializeField] private Text PrizeMoney;
    [SerializeField] private Text PrizeRequierment;
    [SerializeField] private Text mapLaps;
    [SerializeField] private GameObject mapLapsHeader;
    [SerializeField] private Button playButton;
    private float timeTrialTime;
    private int prizeMoney;
    private string sceneToLoad;
    private string trackType;
    private string raceType;
    private int laps;

    /// <param 
    /// name="map"> Map: A scriptable object containing information about a level.
    /// </param>

    /// <summary>
    ///  This function is called to change the text and images on screen to those of a scriptableobject.
    /// </summary>
    public void DisplayMap(Map map)
    {
        //Some values and texts are saved as variebles to be used in other functions
        prizeMoney = int.Parse(map.prizeMoney);
        sceneToLoad = map.sceneToLoad.name;
        trackType = map.trackType;
        raceType = map.raceType;

        //This changes the text on screen to reflect the map shown
        mapName.text = map.mapName;
        mapdescription.text = map.mapDescrition;
        PrizeMoney.text = map.prizeMoney+" $";
        mapImage.sprite = map.mapImage;


        //Depending on the tracktype some text is removed or replaced
        if(map.trackType == "circuit")
        {
            mapLapsHeader.SetActive(true);
            mapLaps.text = map.laps.ToString();
            laps = map.laps;
        }
        else
        {
            mapLapsHeader.SetActive(false);
            laps = 1;
        }

        //Depending on the racetype some text is changed to work better.
        if (map.raceType == "vs")
        {
            PrizeRequierment.text = "Finish in 1st place";
        }
        else if(map.raceType == "timetrial")
        {
            timeTrialTime = map.timeRequierment;
            PrizeRequierment.text = "Finish within " + TimeSpan.FromSeconds(timeTrialTime).ToString(@"mm\:ss");
        }
        else
        {
            PrizeRequierment.text = "Nothing";
        }
    }

    /// <summary>
    ///  This function is called by a button, and saves the necessary information about the level to the next scene.
    /// </summary>
    public void StartButton()
    {
        //PlayerPrefs are used because they can store information across diffrent scenes

        PlayerPrefs.SetString("MapName", mapName.text);
        PlayerPrefs.SetFloat("MapTime", timeTrialTime);
        PlayerPrefs.SetInt("MapLaps", laps);
        PlayerPrefs.SetInt("MapMoney", prizeMoney);
        PlayerPrefs.SetString("MapToLoad", sceneToLoad);
        PlayerPrefs.SetString("TrackType", trackType);
        PlayerPrefs.SetString("RaceType", raceType);
    }
}
