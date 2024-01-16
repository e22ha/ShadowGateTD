using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI starsText;

    private void Update()
    {
        UpdateStarsUI();//TODO Not put inside the Update method later
    }

    public void UpdateStarsUI()
    {
        int sum = 0;

        for(int i = 1; i < 14; i++)
        {
            sum += PlayerPrefs.GetInt("Lv" + i.ToString());//Add the level 1 stars number, level 2 stars number.....
        }

        starsText.text = sum + "/" + 27;
    }

    public void GoSetting()
    {
        SceneTransition.SwitchToScene("MainSettings");
    }

    public void GoMain()
    {
        SceneTransition.SwitchToScene("MainMenu");
    }

    public void GoShop()
    {
        SceneTransition.SwitchToScene("Shop");
    }

    public void GoMenu()
    {
        SceneTransition.SwitchToScene("StoryMenu");
    }
}
