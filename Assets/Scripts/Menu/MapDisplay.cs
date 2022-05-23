using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text mapName;
    [SerializeField] private Text mapdescription;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;

    public void DisplayMap(Map map)
    {
        mapName.text = map.mapName;
        mapdescription.text = map.mapDescrition;
        mapImage.sprite = map.mapImage;


        
        //PlayerPrefs.SetString("MapType", map.mapType);
        //PlayerPrefs.SetString("MapToLoad", map.sceneToLoad.name);
        
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("MapType", map.mapType));
        playButton.onClick.AddListener(() => PlayerPrefs.SetString("MapToLoad", map.sceneToLoad.name));
    }
}
