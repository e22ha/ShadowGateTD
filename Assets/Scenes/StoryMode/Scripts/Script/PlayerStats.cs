using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    
    //script for update count of lives, money and score
    
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Score;
    public int startScore = 0;
    

    private void Awake ()
    {
        Money = startMoney;
        Lives = startLives;
        Score = startScore;
    }
    
    public static void UpdateMoney(int money)
    {
        Money += money;
    }

    public static void UpdateScore(int killPoint)
    {
        Score += killPoint;
    }
    
    public static void UpdateLives(int livePoint)
    {
        Lives -= livePoint;
        if (Lives > 0) return;
        Debug.LogWarning("You lose! lose window");
        Time.timeScale = 0;
    }
}