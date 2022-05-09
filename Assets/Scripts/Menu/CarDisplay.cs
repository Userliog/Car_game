using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] public Text carName;
    [SerializeField] public Text carDescription;

    [Header("Values")]
    [SerializeField] public Text carPrice;

    [SerializeField] public bool carOwned;

    [Header("Spawnpoint")]
    [SerializeField] public Transform carContainer;

    [Header("Stuff")]
    [SerializeField] private Button playButton;

    public void DisplayCar(Car car)
    {
        carName.text = car.carName;
        carDescription.text = car.carDescription;
        carPrice.text = car.carPrice + " $";

        if (carContainer.childCount > 0)
        {
            Destroy(carContainer.GetChild(0).gameObject);
        }

        Instantiate(car.carModel, carContainer.position, carContainer.rotation, carContainer);
        
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(PlayerPrefs.GetString("MapToLoad")));
    }
}
