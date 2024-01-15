using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneTransition.SwitchToScene("StoryMenu");
    }

    public void GoChallenger()
    {
        SceneTransition.SwitchToScene("ChallengerMode");
    }

    public void GoEditor()
    {
        SceneTransition.SwitchToScene("EditorMode");
    }
}
