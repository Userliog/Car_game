using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Linq;
using UnityEngine.UI;
using System;

public class LoadCar : MonoBehaviour
{
    [SerializeField] private List<GameObject> routeList;
    [SerializeField] private ScriptableObject[] ScriptableObjects;
    public Transform spawnPoint;
    public CinemachineFreeLook vcam;

    public bool stopwatchOn = false;
    public float currentTime = 0;
    [SerializeField] private GameObject timerBase;
    public Text timeText;

    void Start()
    {
        foreach(GameObject x in routeList)
        {
            x.SetActive(false);
        }

        GameObject route = routeList.Where(obj => obj.name == PlayerPrefs.GetString("MapName")).SingleOrDefault();
        if(route != null)
        {
            route.SetActive(true);
            spawnPoint = route.transform.GetChild(0);
        }
        

        print(PlayerPrefs.GetString("MapName"));

        print(route);

        if (PlayerPrefs.GetString("RaceType") == "freeroam")
        {
            timerBase.SetActive(false);
        }

        int selectedCar = PlayerPrefs.GetInt("CarToLoad");
        PlaceCar((Car)ScriptableObjects[selectedCar]);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (stopwatchOn == true)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.ToString(@"mm\:ss\:fff");
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

    public void StartStopwatch()
    {
        if (PlayerPrefs.GetString("RaceType") != "freeroam")
        {
            stopwatchOn = true;
        } 
    }
    public float StopStopwatch()
    {
        stopwatchOn = false;
        return currentTime;
    }
}