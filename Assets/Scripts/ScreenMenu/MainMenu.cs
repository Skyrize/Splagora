using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;


public class MainMenu : MonoBehaviour
{
    public XboxController controller;
    public GameObject Tuto;

    public void Update()
    {
        if (Tuto != null)
        {
            if (XCI.GetButtonDown(XboxButton.Start, controller) && Tuto.gameObject.activeInHierarchy == true)
            {
                startPlaying();
            }
        }
    }
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
