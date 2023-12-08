using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public void GoMain()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void GoSetting()
    {
        SceneManager.LoadScene("MainSettings");
    }

    public void GoFAQ()
    {
        SceneManager.LoadScene("MainFAQ");
    }
    
    public void GoStory()
    {
        SceneManager.LoadScene("StoryMode");
    }

    public void GoChallenger()
    {
        SceneManager.LoadScene("ChallengerMode");
    }

    public void GoEditor()
    {
        SceneManager.LoadScene("EditorMode");
    }
}
