using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI_Manager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject pauseBtn;

    private int currentStarsNum = 0;
    public int levelIndex;

    public void pauseMenuOn()
    {
        Time.timeScale = 0;

        pauseUI.SetActive(true);
        pauseBtn.SetActive(false);
    }

    public void pauseMenuOff()
    {
        Time.timeScale = 1;

        settingsMenu.SetActive(false);
        pauseUI.SetActive(false);
        pauseBtn.SetActive(true);
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
        SceneManager.LoadScene("StoryMenu");
    }

    public void restartGame()
    {
    }

    public void PressStarsButton(int _starsNum)
    {
        currentStarsNum = _starsNum;

        if(currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _starsNum);
        }

        //MARKER Each level has saved their own stars number
        //CORE PLayerPrefs.getInt("KEY", "VALUE"); We can use the KEY to find Our VALUE
        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, _starsNum));

        goMain();
    }
}
