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

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(map.sceneToLoad.name));
    }
}
