using System.Collections;
using Scenes.StoryMode.LevelScripts.Script;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.StoryMode.LevelScripts
{
    public class GameManager : MonoBehaviour
    {
        public WaveSpawner waveSpawner;
        public PlayerStats playerStats;

        private int _defeatedEnemies;
        private int _notDefeatedEnemies;


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
            // Check for lose condition
            if (playerStats.GetLives() <= 0)
            {
                GameOver();
            }

            // Check for win condition
            if(!AllWavesCompleted()) return;
            if(AllEnemiesDestroyed()) GameWin();
        }

        private void GameWin()
        {
            Debug.Log("YouWin!");
            var grade = CalculateGrade(playerStats.GetLives());
            Debug.Log($"Grade: {grade} stars");
            StartCoroutine(DelayedLevelTransition());
        }

        private void GameOver()
        {
            Debug.Log("YouLose!");
            StartCoroutine(DelayedLevelTransition());
        }

        // Method to calculate the grade based on remaining lives
        private static int CalculateGrade(int remainingLives)
        {
            return remainingLives switch
            {
                >= 5 => 1,
                >= 1 => 2,
                _ => 3
            };
        }
        
        private IEnumerator DelayedLevelTransition()
        {
            // Подождем 2 секунды
            yield return new WaitForSeconds(2f);

            // 3. Загрузим сцену со всеми уровнями (StoryMenu)
            SceneManager.LoadScene("StoryMenu");
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