using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResultsDisplay : MonoBehaviour
{
    [SerializeField] private Text header;
    [SerializeField] private Text currentResult;
    [SerializeField] private Text PrizeRequierment;
    [SerializeField] private Text PrizeMoney;
    [SerializeField] private Text currentBalance;
    [SerializeField] private LoadCar stopwatch;
    private int newMoney;
    
    //
    //Keeep!! Keep!! Keep!!
    //


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void DisplayResultsTimeTrial()
    {
        Time.timeScale = 0.5f;
        gameObject.SetActive(true);
        float stopWatchTime = stopwatch.StopStopwatch();
        float timeToBeat = PlayerPrefs.GetFloat("MapTime");
        if (stopWatchTime <= timeToBeat)
        {
            header.text = "You beat the time";
            newMoney = PlayerPrefs.GetInt("MapMoney");
        }
        else if(stopWatchTime > timeToBeat && stopWatchTime <= (timeToBeat*1.15))
        {
            header.text = "You almost beat the time";
            newMoney = (PlayerPrefs.GetInt("MapMoney")) / 2;
        }else
        {
            header.text = "You lost";
            newMoney = 0;
        }

        currentResult.text = "You finished in " + TimeSpan.FromSeconds(stopWatchTime).ToString(@"mm\:ss\:fff");
        PrizeMoney.text = "You earned " + newMoney + " $";
        PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + newMoney));
        PrizeRequierment.text = "The time to beat was " + TimeSpan.FromSeconds(timeToBeat).ToString(@"mm\:ss\:fff");
        currentBalance.text = "Your current balance is " + PlayerPrefs.GetInt("money") + " $";
    }
    public void DisplayResultsRace(string Winner)
    {
        Time.timeScale = 0.5f;
        gameObject.SetActive(true);
        float stopWatchTime = stopwatch.StopStopwatch();
        //float stopWatchTime = StopStopwatch();
        //float timeToBeat = PlayerPrefs.GetFloat("MapTime");
        if (Winner == "Player")
        {
            header.text = "You beat the opponent";
            currentResult.text = "You finished in 1st place, it took you "+ TimeSpan.FromSeconds(stopWatchTime).ToString(@"mm\:ss\:fff");
            int newMoney = PlayerPrefs.GetInt("MapMoney");
            PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + newMoney));
            PrizeMoney.text = "You earned " + newMoney + " $";
        }
        else
        {
            header.text = "You lost";
            currentResult.text = "You finished in last place, it took you " + TimeSpan.FromSeconds(stopWatchTime).ToString(@"mm\:ss\:fff");
            int newMoney = PlayerPrefs.GetInt("MapMoney");
            PrizeMoney.text = "You earned 0 $";
        }
        PrizeRequierment.text = "The position to beat was 1st place";
        currentBalance.text = "Your current balance is " + PlayerPrefs.GetInt("money") + " $";
    }
    public void Return()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
