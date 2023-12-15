using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.StoryMode.LevelScripts
{
    public class GameManager : MonoBehaviour
    {
        
        public WaveSpawner waveSpawner;

        public PlayerStats playerStats;

        private int _defeatedEnemies = 0;
        private int _notDefeatedEnemies = 0;


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
            Debug.Log($"(not)Defeated Enemies: ({_notDefeatedEnemies}){_defeatedEnemies}/{waveSpawner.TotalExpectedEnemies}");
        }

        // Update is called once per frame
        private void Update()
        {   
            // Check for lose condition
            if (playerStats.GetLives() <= 0)
            {
                GameOver();
            }

            // Check for win condition
            if (waveSpawner.LastWaveCompleted && AllEnemies())
            {
                GameWin();
            }
        }

        private void GameWin()
        {
            Debug.Log("YouWin!");
            Time.timeScale = 0;
        }

        private void GameOver()
        {
            Debug.Log("YouLose!");
            Time.timeScale = 0;
        }

        // Method to check if all enemies are defeated
        private bool AllEnemies()
        {
            return _defeatedEnemies+_notDefeatedEnemies >= waveSpawner.TotalExpectedEnemies;
        }
    }
}
