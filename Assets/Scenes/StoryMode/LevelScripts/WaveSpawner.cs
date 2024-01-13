using System.Collections;
using System.Collections.Generic;
using Scenes.StoryMode.LevelScripts.Script;
using Scenes.StoryMode.Scripts.Script;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.StoryMode.LevelScripts
{
    public class WaveSpawner : MonoBehaviour
    {
        public Transform enemyPrefab;
        public Transform enemyFlyPrefab;
        private Vector3 _spanPoint;

        private float _countdown = 2f;

        private int _maxWaveNumber;

        [Header("UI")] public TMP_Text waveBarText;

        public LevelConfig levelConfig; // Reference to the level configuration asset
        public GameManager gameManager; // Reference to the GameManager
        private Vector3 _pathPos;

        public int TotalExpectedEnemies { get; private set; }

        public int CurrentWaveNumber { get; private set; }

        private void Start()
        {
            Debug.Log($"{name} is Awake!");
            _maxWaveNumber = levelConfig.waveConfigurations.Length;
            ControlWaveBar();
            Time.timeScale = 1f;

            PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);

            _pathPos = levelConfig.pathConfiguration.pathNodes[0];
            _spanPoint = new Vector3(_pathPos.x * 0.8f, 0.4f, _pathPos.z * 0.8f);

            // Set the initial total expected enemies
            SetTotalExpectedEnemies();
        }

        private void SetTotalExpectedEnemies()
        {
            TotalExpectedEnemies = 0;

            for (var i = 0; i < _maxWaveNumber; i++)
            {
                TotalExpectedEnemies += levelConfig.waveConfigurations[i].numberOfEnemies +
                                        levelConfig.waveConfigurations[i].numberOfFlyEnemies;
            }

            Debug.Log($"Total enemies: {TotalExpectedEnemies}");
        }

        private void ControlWaveBar()
        {
            waveBarText.text = $"Волна {CurrentWaveNumber}/{_maxWaveNumber}";
        }

        private void FixedUpdate()
        {
            if (CurrentWaveNumber < _maxWaveNumber)
            {
                if (_countdown <= 0f)
                {
                    _countdown = levelConfig.waveConfigurations[CurrentWaveNumber].numberOfEnemies +
                                 levelConfig.waveConfigurations[CurrentWaveNumber].numberOfFlyEnemies +
                                 levelConfig.separationTime;

                    StartCoroutine(SpawnWave(CurrentWaveNumber));
                    CurrentWaveNumber++;
                    ControlWaveBar();
                }
            }
            else
            {
                // Notify the GameManager that the last wave is completed
                LastWaveCompleted = true;
            }

            _countdown -= Time.deltaTime;
        }

        public bool LastWaveCompleted { get; set; }


        private IEnumerator SpawnWave(int waveNumber)
        {
            var enemyTypes = new List<int>();

            // Add ground enemies to the list
            for (var i = 0; i < levelConfig.waveConfigurations[waveNumber].numberOfEnemies; i++)
            {
                enemyTypes.Add(0);
            }

            // Add flying enemies to the list
            for (var j = 0; j < levelConfig.waveConfigurations[waveNumber].numberOfFlyEnemies; j++)
            {
                enemyTypes.Add(1);
            }

            // Shuffle the list to randomize the order of enemy types
            ShuffleList(enemyTypes);

            // Spawn enemies based on the randomized list
            foreach (var enemyType in enemyTypes)
            {
                SpawnEnemy(enemyType, levelConfig.pathConfiguration.pathNodes);
                yield return new WaitForSeconds(levelConfig.separationTime);
            }
        }

        private static void ShuffleList<T>(IList<T> list)
        {
            var n = list.Count;
            for (var i = 0; i < n - 1; i++)
            {
                var randomIndex = Random.Range(i, n);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }

        private void SpawnEnemy(int type, Vector3[] pathNodes)
        {
            switch (type)
            {
                case 0:
                    var enemy = Instantiate(enemyPrefab, _spanPoint, Quaternion.identity).GetComponent<Enemy>();
                    enemy.Initialize(pathNodes, 0.8f); // Pass the pathNodes and spacing to the enemy
                    enemy.OnDefeated += OnEnemyDefeated;
                    enemy.OnNotDefeated += OnEnemyNotDefeated;
                    break;
                case 1:
                    var enemyFly = Instantiate(enemyFlyPrefab, _spanPoint, Quaternion.identity).GetComponent<Enemy>();
                    enemyFly.Initialize(pathNodes, 0.8f); // Pass the pathNodes and spacing to the enemy
                    enemyFly.OnDefeated += OnEnemyDefeated;
                    enemyFly.OnNotDefeated += OnEnemyNotDefeated;
                    break;
            }
        }

        // Event handler for enemy defeated event
        private void OnEnemyDefeated()
        {
            gameManager.EnemyDefeated();
        }

        private void OnEnemyNotDefeated()
        {
            gameManager.EnemyNotDefeated();
        }
    }
}