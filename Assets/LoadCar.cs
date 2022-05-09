using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCar : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] ScriptableObjects;
    public Transform spawnPoint;
    //private Car car;

    void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("CarToLoad");
        PlaceCar((Car)ScriptableObjects[selectedCar]);
        //car = ScriptableObjects[selectedCar];
        //Instantiate(car.carModel, spawnPoint.position, spawnPoint.rotation, spawnPoint);
    }
    public void PlaceCar(Car car)
    {
        if (spawnPoint.childCount > 0)
        {
            Destroy(spawnPoint.GetChild(0).gameObject);
        }

        Instantiate(car.carModel, spawnPoint.position, spawnPoint.rotation, spawnPoint);
    }
}