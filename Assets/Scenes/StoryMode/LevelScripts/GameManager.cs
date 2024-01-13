using Scenes.StoryMode.LevelScripts.Script;
using UnityEngine;

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
            if (AllWavesCompleted() && AllEnemiesDestroyed())
            {
                GameWin();
            }
        }
// Method to handle game win
        public void GameWin()
        {
            Debug.Log("YouWin!");

            // Calculate the grade based on remaining lives
            int remainingLives = playerStats.GetLives();
            int grade = CalculateGrade(remainingLives);

            // Print the grade to the console
            Debug.Log($"Grade: {grade} stars");

            // Display the grade in the UI or perform other win-related actions

            Time.timeScale = 0;
        }

        // Method to handle game over
        public void GameOver()
        {
            Debug.Log("YouLose!");

            // Calculate the grade based on remaining lives
            int remainingLives = playerStats.GetLives();
            int grade = CalculateGrade(remainingLives);

            // Print the grade to the console
            Debug.Log($"Grade: {grade} stars");

            // Display the grade in the UI or perform other lose-related actions

            Time.timeScale = 0;
        }

        // Method to calculate the grade based on remaining lives
        private int CalculateGrade(int remainingLives)
        {
            return remainingLives switch
            {
                >= 5 => 1,
                >= 1 => 2,
                _ => 3
            };
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
