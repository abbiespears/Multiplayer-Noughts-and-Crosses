using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OfflineModePressed()
    {
        SceneManager.LoadScene("OfflineGame");
    }
    public void OnlineModePressed()
    {
        SceneManager.LoadScene("Loading");
    }
    public void QuitPressed()
    {
        Application.Quit();
    }
}
