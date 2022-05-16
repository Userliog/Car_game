using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MapOptionsDisplay : MonoBehaviour
{
    [SerializeField] private Text optionType;
    [SerializeField] private Text optionType2;
    [SerializeField] private Text optionDescription;
    [SerializeField] private Image optionImage;
    [SerializeField] private Button doneButton;

    public void DisplayMapOptions(LevelOptions options)
    {
        optionType.text = options.optionType;
        optionDescription.text = options.optionDescription;
        optionImage.sprite = options.optionImage;

        PlayerPrefs.SetString("LevelOptions", options.optionType);
        doneButton.onClick.RemoveAllListeners();
    }
}