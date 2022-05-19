using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LoadCar : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] ScriptableObjects;
    public Transform spawnPoint;
    public CinemachineFreeLook vcam;
    void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("CarToLoad");
        PlaceCar((Car)ScriptableObjects[selectedCar]);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void PlaceCar(Car car)
    {
        if (spawnPoint.childCount > 0)
        {
            Destroy(spawnPoint.GetChild(0).gameObject);
        }

        Instantiate(car.carModel, spawnPoint.position, spawnPoint.rotation, spawnPoint);

        vcam.Follow = GameObject.FindWithTag("Player").transform;
        vcam.LookAt = GameObject.FindWithTag("Player").transform;
    }
}