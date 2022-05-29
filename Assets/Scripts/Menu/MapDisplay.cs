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
    //private string mapType;
    //private string sceneToLoad;
    private int laps;

    public void DisplayMap(Map map)
    {
        mapName.text = map.mapName;
        mapdescription.text = map.mapDescrition;
        PrizeMoney.text = map.prizeMoney+" $";
        
        mapImage.sprite = map.mapImage;
        //mapType = map.mapType;
        //sceneToLoad = map.sceneToLoad.name;

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
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("RaceType", map.raceType));
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("TrackType", map.trackType));
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("MapToLoad", map.sceneToLoad.name));
        playButton.onClick.AddListener(() => PlayerPrefs.SetInt("MapLaps", map.laps));
        playButton.onClick.AddListener(() => PlayerPrefs.SetInt("MapMoney", int.Parse(map.prizeMoney)));
    }
    public void StartButton()
    {
        PlayerPrefs.SetString("MapName", mapName.text);
        //PlayerPrefs.SetString("MapMoney", PrizeMoney.text);
        PlayerPrefs.SetFloat("MapTime", timeTrialTime);
        //PlayerPrefs.SetString("MapType", mapType);
        //PlayerPrefs.SetString("MapToLoad", sceneToLoad);
        //PlayerPrefs.SetInt("MapLaps", int.Parse(mapLaps.text));

        PlayerPrefs.SetInt("MapLaps", laps);


        print("Name: "+PlayerPrefs.GetString("MapName"));
        print("Money: "+PlayerPrefs.GetInt("MapMoney"));
        print("Time: " + PlayerPrefs.GetFloat("MapTime"));
        print("Type: " + PlayerPrefs.GetString("MapType"));
        print("ToLoad: " + PlayerPrefs.GetString("MapToLoad"));
        print("Laps: " + PlayerPrefs.GetInt("MapLaps"));
    }
}
