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

    private void Awake()
    {
        //The display is hidden at the start of the game
        gameObject.SetActive(false);
    }

    /// <summary>
    ///  This function is called to display the results after a timetrial.
    /// </summary>
    public void DisplayResultsTimeTrial()
    {
        //The time is slowed down and the UI is visable.
        Time.timeScale = 0.5f;
        gameObject.SetActive(true);
        //The time from when the player hit the start button is stoped and compared to the time to beat, and if its lower, then the text displays that the player won and the player recives money.
        float stopWatchTime = stopwatch.StopStopwatch();
        float timeToBeat = PlayerPrefs.GetFloat("MapTime");
        if (stopWatchTime <= timeToBeat)
        {
            header.text = "You beat the time";
            newMoney = PlayerPrefs.GetInt("MapMoney");
        }
        else if(stopWatchTime > timeToBeat && stopWatchTime <= (timeToBeat*1.1)) //If the player almost beats the time, then the player gets half of the prize money.
        {
            header.text = "You almost beat the time";
            newMoney = (PlayerPrefs.GetInt("MapMoney")) / 2;
        }else //If the time is slower still then the text sais "You lost", and the player recives no money.
        {
            header.text = "You lost";
            newMoney = 0;
        }

        //The rest of the text thats identical between them is shown here.
        currentResult.text = "You finished in " + TimeSpan.FromSeconds(stopWatchTime).ToString(@"mm\:ss\:fff");
        PrizeMoney.text = "You earned " + newMoney + " $";
        PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + newMoney));
        PrizeRequierment.text = "The time to beat was " + TimeSpan.FromSeconds(timeToBeat).ToString(@"mm\:ss\:fff");
        currentBalance.text = "Your current balance is " + PlayerPrefs.GetInt("money") + " $";
    }

    /// <param 
    /// name="Winner"> String: The one who passes the finnishline first.
    /// </param>
    /// <summary>
    ///  This function is called to display the results after a race against an opponent.
    /// </summary>
    public void DisplayResultsRace(string Winner)
    {
        //The time is slowed down, the timer is stopped and the UI is visable.
        Time.timeScale = 0.5f;
        gameObject.SetActive(true);
        float stopWatchTime = stopwatch.StopStopwatch();

        //If the input string, "Winner", is the player, then the player won the race and gets the reward, and all the information is displayed.
        if (Winner == "Player")
        {
            header.text = "You beat the opponent";
            currentResult.text = "You finished in 1st place, it took you "+ TimeSpan.FromSeconds(stopWatchTime).ToString(@"mm\:ss\:fff");
            int newMoney = PlayerPrefs.GetInt("MapMoney");
            PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + newMoney));
            PrizeMoney.text = "You earned " + newMoney + " $";
        }
        else //But if the opponent won then the player lost, and that information is displayed.
        {
            header.text = "You lost";
            currentResult.text = "You finished in last place, it took you " + TimeSpan.FromSeconds(stopWatchTime).ToString(@"mm\:ss\:fff");
            PrizeMoney.text = "You earned 0 $";
        }
        PrizeRequierment.text = "The position to beat was 1st place";
        currentBalance.text = "Your current balance is " + PlayerPrefs.GetInt("money") + " $";
    }
    
    /// <summary>
    ///  This function is called to load the main menu.
    /// </summary>
    public void Return()
    {
        //The time is resumed to normal speed again.
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    ///  This function is called to restart the level.
    /// </summary>
    public void Restart()
    {
        //The time is resumed to normal speed again.
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
