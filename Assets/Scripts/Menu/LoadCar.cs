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
    [SerializeField] private GameObject pauseMenuUI;
    public Transform spawnPoint;
    public CinemachineFreeLook vcam;

    private bool gameIsPaused = false;
    public bool stopwatchOn = false;
    public float currentTime = 0;
    [SerializeField] private GameObject timerBase;
    public Text timeText;

    void Start()
    {
        //The pause menue is hiden
        pauseMenuUI.SetActive(false);

        //Hides all the avalible routes
        foreach (GameObject x in routeList)
        {
            x.SetActive(false);
        }

        //unhides the one the player selected
        GameObject route = routeList.Where(obj => obj.name == PlayerPrefs.GetString("MapName")).SingleOrDefault();
        if(route != null)
        {
            //Spawns the player in a transform specific to that route
            route.SetActive(true);
            spawnPoint = route.transform.GetChild(0);
        }

        //hides the timer if the racetype is freeroam, since its not needed
        if (PlayerPrefs.GetString("RaceType") == "freeroam")
        {
            timerBase.SetActive(false);
        }

        //Gets the selected car from a list and spawns it using the function "PlaceCar"
        int selectedCar = PlayerPrefs.GetInt("CarToLoad");
        PlaceCar((Car)ScriptableObjects[selectedCar]);
    }
    void Update()
    {
        //This checks if the player presses "R", and if so reloads the level
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //This pauses respective unpauses the pause menu depending on if its open or not
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        //This increasses the timer based on game time (and not real time), and displays it in minutes, seconds and milliseconds in a text field
        if (stopwatchOn == true)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.ToString(@"mm\:ss\:fff");
    }

    /// <param
    /// name="car"> Car: A scriptableobject containing information about a car.
    /// </param>
    /// <summary>
    ///  This function instantiates a car in a spawnpoint and attatches a camera to the player.
    /// </summary>
    public void PlaceCar(Car car)
    {
        //This destroys any thing thats already in the spawnpoint
        if (spawnPoint.childCount > 0)
        {
            Destroy(spawnPoint.GetChild(0).gameObject);
        }

        Instantiate(car.carModel, spawnPoint.position, spawnPoint.rotation, spawnPoint);

        //This makes the camera follow and move around the player
        vcam.Follow = GameObject.FindWithTag("Player").transform;
        vcam.LookAt = GameObject.FindWithTag("Player").transform;
    }

    /// <summary>
    ///  This function starts the stopwatch, aslong as the racetype isnt freeroam.
    /// </summary>
    public void StartStopwatch()
    {
        if (PlayerPrefs.GetString("RaceType") != "freeroam")
        {
            stopwatchOn = true;
        } 
    }

    /// <summary>
    ///  This function stops the stopwatch, and returns the time it stopped at.
    /// </summary>
    public float StopStopwatch()
    {
        stopwatchOn = false;
        return currentTime;
    }

    /// <summary>
    ///  This function pauses the game and opens a pause menu.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }

    /// <summary>
    ///  This function resumes the game and closes the pause menu.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    /// <summary>
    ///  This function loads the main menu.
    /// </summary>
    public void ReturnToMainMenu()
    {
        //The time is resumed to normal speed
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    ///  This function reloads the current scene.
    /// </summary>
    public void Restart()
    {
        //The time is resumed to normal speed
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}