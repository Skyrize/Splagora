using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void startPlaying()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("ScreenMenu", 0);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
