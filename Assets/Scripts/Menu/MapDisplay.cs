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
    [SerializeField] private Button playButton;
    private float timeTrialTime;

    public void DisplayMap(Map map)
    {
        mapName.text = map.mapName;
        mapdescription.text = map.mapDescrition;
        PrizeMoney.text = map.prizeMoney+" $";
        mapImage.sprite = map.mapImage;

        if(map.mapType == "vs")
        {
            PrizeRequierment.text = "Finish "+map.prizeRequierment+" place";
        }
        else if(map.mapType == "timetrial")
        {
            timeTrialTime = float.Parse(map.prizeRequierment);
            PrizeRequierment.text = "Finish within " + TimeSpan.FromSeconds(timeTrialTime).ToString(@"mm\:ss");
        }
        else
        {
            PrizeRequierment.text = "Nothing";
        }
        




        //PlayerPrefs.SetString("MapType", map.mapType);
        //PlayerPrefs.SetString("MapToLoad", map.sceneToLoad.name);

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("MapMoney", map.prizeMoney));
        playButton.onClick.AddListener(() => PlayerPrefs.SetFloat("MapTime", timeTrialTime));
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("MapType", map.mapType));
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("MapToLoad", map.sceneToLoad.name));
    }
}
