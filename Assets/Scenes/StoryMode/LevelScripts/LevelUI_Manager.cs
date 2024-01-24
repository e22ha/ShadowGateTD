using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI_Manager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject settingsMenu;
    public Canvas canvasUI;
    public GameObject pauseMenu;
    public GameObject pauseBtn;

    public int levelIndex;

    public void pauseMenuOn()
    {
        Time.timeScale = 0f;

        pauseUI.SetActive(true);
        pauseBtn.SetActive(false);
        canvasUI.sortingOrder = 2;
    }

    public void pauseMenuOff()
    {
        Time.timeScale = 1f;

        settingsMenu.SetActive(false);
        pauseUI.SetActive(false);
        pauseBtn.SetActive(true);
        canvasUI.sortingOrder = 0;
    }

    public void settingsMenuOn()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void settingsMenuOff()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void goMain()
    {
        SceneTransition.SwitchToScene("StoryMenu");
    }

    public void RestartGame()
    {
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().name);
    }
}