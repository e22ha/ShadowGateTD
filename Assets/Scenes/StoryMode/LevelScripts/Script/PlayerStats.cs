using Scenes.StoryMode.Scripts.Script;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    //script for update count of lives, money and score

    private static PlayerStats Instance { get; set; }
    
    public static int Money;
    public int startMoney = 999;

    public static int Lives;
    public int startLives = 10;

    public UpdateInfoCount updateInfoCount;  // Reference to the UpdateInfoCount script

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }

        Money = startMoney;
        Lives = startLives;
    }

    
    public static void UpdateMoney(int money)
    {
        Money += money;
        UpdateUI();
    }
    public static bool CanAfford(int cost)
    {
        return Money >= cost;
    }

    public static void UpdateLives(int livePoint)
    {
        Lives -= livePoint;
        Debug.Log("Hit!");

        // Update UI even if lives reach zero
        UpdateUI();

        if (Lives > 0) return;
        Debug.Log("You lose! lose window");
        Time.timeScale = 0;
    }

    private static void UpdateUI()
    {
        Instance.updateInfoCount.UpdateUI();
    }

}