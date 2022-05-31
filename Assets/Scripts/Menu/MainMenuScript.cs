using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    /// <summary>
    ///  This function is called by the quit button and quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
