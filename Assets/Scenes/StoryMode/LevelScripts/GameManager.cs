using System.Collections;
using Scenes.StoryMode.LevelScripts.Script;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.StoryMode.LevelScripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject WinWindow;
        public GameObject LoseWindow;
        public Canvas canvasUI;
        public WaveSpawner waveSpawner;
        public PlayerStats playerStats;
        public int levelIndex;

        private int _defeatedEnemies;
        private int _notDefeatedEnemies;
        private bool _endAnnounced = false;


        public void EnemyNotDefeated()
        {
            _notDefeatedEnemies++;
        }

        // Method to be called when an enemy is defeated
        public void EnemyDefeated()
        {
            _defeatedEnemies++;

            // You can add additional logic here based on your game's requirements
            UpdateDefeatedEnemiesCount();
        }

        // Method to update the count of defeated enemies
        private void UpdateDefeatedEnemiesCount()
        {
            Debug.Log(
                $"(not)Defeated Enemies: ({_notDefeatedEnemies}){_defeatedEnemies}/{waveSpawner.TotalExpectedEnemies}");
        }

        // Update is called once per frame
        private void Update()
        {
            if (_endAnnounced) return;
            // Check for lose condition
            if (playerStats.GetLives() <= 0)
            {
                GameOver();
            }

            // Check for win condition
            if (!AllWavesCompleted()) return;
            if (AllEnemiesDestroyed()) GameWin();
        }

        public void GameWin()
        {
            Debug.Log("YouWin!");

            _endAnnounced = true;
            
            int starsNum = CalculateGrade(playerStats.GetLives());
            PlayerPrefs.SetInt("StarsTemp", starsNum);
            Debug.Log($"Grade: {starsNum} stars");

            if (starsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
            {
                PlayerPrefs.SetInt("Lv" + levelIndex, starsNum);
            }

            Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, starsNum));

            canvasUI.sortingOrder = 2;      
            WinWindow.SetActive(true);
        }

        private void GameOver()
        {
            Debug.Log("YouLose!");

            _endAnnounced = true;

            PlayerPrefs.SetInt("StarsTemp", 0);

            canvasUI.sortingOrder = 2;
            LoseWindow.SetActive(true);
        }

        // Method to calculate the grade based on remaining lives
        private static int CalculateGrade(int remainingLives)
        {
            return remainingLives switch
            {
                >= 10 => 3,
                >= 6 => 2,
                _ => 1
            };
        }
        
        private IEnumerator DelayedLevelTransition()
        {
            // Подождем 2 секунды
            yield return new WaitForSeconds(2f);

            // 3. Загрузим сцену со всеми уровнями (StoryMenu)
            SceneTransition.SwitchToScene("StoryMenu");
        }

        // Method to check if all waves are completed
        private bool AllWavesCompleted()
        {
            return waveSpawner.LastWaveCompleted;
        }

        // Method to check if all enemies are destroyed
        private static bool AllEnemiesDestroyed()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            return enemies.Length == 0;
        }
    }
}