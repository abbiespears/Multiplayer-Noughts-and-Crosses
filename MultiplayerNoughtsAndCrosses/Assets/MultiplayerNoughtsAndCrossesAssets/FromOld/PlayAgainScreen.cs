using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the functionality of the Main Menu Screen.
/// This class manages the user interaction with the play button, allowing the game to be started by loading the game scene.
/// </summary>
public class PlayAgainScreen : MonoBehaviour
{
    /// <summary>
    /// Called when the "Play" button is pressed.
    /// This method loads the game scene, effectively starting the game.
    /// </summary>
    public void PlayPressed()
    {
        SceneManager.LoadScene("OfflineGame");
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
