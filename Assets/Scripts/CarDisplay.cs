using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] public Text carName;
    [SerializeField] public Text carDescrition;

    [Header("Values")]
    [SerializeField] public Text carPrice;

    [SerializeField] public bool owned;

    [Header("3D Model")]
    [SerializeField] public Transform carModel;
    
    public void Displaycar(Car car)
    {
        carName.text = car.carName;
        cardescription.text = car.carDescrition;
        carPrice.text = car.carPrice + " $";
        carOwned = car.carOwned

            if (carHolder.childCount > 0)
            {
                Destroy(carHolder.GetChild(0).gameObject);
            }
            
        playButton.onClick.RemoveAllListeners()
        playButton.onClick.AddListener(() => SceneManager.LoadScen(map.scenToLoad.name));
    }
}
