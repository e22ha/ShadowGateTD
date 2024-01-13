using UnityEngine;

namespace Scenes.StoryMode.LevelScripts.Script
{
    public class PlayerStats : MonoBehaviour
    {
    
        //script for update count of lives, money and score

        private static PlayerStats Instance { get; set; }
        
        public LevelConfig levelConfig;

    
        public static int Money;

        public static int Lives;

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

            Money = levelConfig.startMoney;
            Lives = levelConfig.startLives;
        }

    
        public static void UpdateMoney(int money)
        {
            Money += money;
            UpdateUI();
        }
        public static bool CanAfford(int cost)
        {
            Debug.Log($"Money: {Money}; Cost: {cost};");
            return Money >= cost;
        }

        public static void UpdateLives(int livePoint)
        {
            Lives -= livePoint;

            // Update UI even if lives reach zero
            UpdateUI();
        }

        private static void UpdateUI()
        {
            Instance.updateInfoCount.UpdateUI();
        }

        public int GetLives()
        {
            return Lives;
        }
    }
}