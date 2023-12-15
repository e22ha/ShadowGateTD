using System.Collections;
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
        public Transform spanPoint;

        private float _countdown = 2f;

        private int _waveNumber;
        private int _maxWaveNumber;

        [Header("UI")] public TMP_Text waveBarText;

        public LevelConfig levelConfig; // Reference to the level configuration asset
        public GameManager gameManager; // Reference to the GameManager


        private int _totalExpectedEnemies; // Total expected enemies in the current wave

        public int TotalExpectedEnemies
        {
            get { return _totalExpectedEnemies; }
        }

        public int CurrentWaveNumber
        {
            get { return _waveNumber; }
        }

        private void Start()
        {
            Debug.Log($"{name} is Awake!");
            _maxWaveNumber = levelConfig.waveConfigurations.Length;
            ControlWaveBar();
            Time.timeScale = 1f;

            PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);


            // Set the initial total expected enemies
            SetTotalExpectedEnemies();
        }


        private void SetTotalExpectedEnemies()
        {
            _totalExpectedEnemies = 0;

            for (var i = 0; i < _maxWaveNumber; i++)
            {
                _totalExpectedEnemies += levelConfig.waveConfigurations[i].numberOfEnemies +
                                         levelConfig.waveConfigurations[i].numberOfFlyEnemies;
            }
            Debug.Log($"Total enemies: {_totalExpectedEnemies}");
        }

        private void ControlWaveBar()
        {
            waveBarText.text = $"Волна {_waveNumber}/{_maxWaveNumber}";
        }

        private void FixedUpdate()
        {
            if (_waveNumber < _maxWaveNumber)
            {
                if (_countdown <= 0f)
                {
                    _countdown = levelConfig.waveConfigurations[_waveNumber].numberOfEnemies +
                                 levelConfig.waveConfigurations[_waveNumber].numberOfFlyEnemies +
                                 levelConfig.separationTime;

                    StartCoroutine(SpawnWave(_waveNumber));
                    _waveNumber++;
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
            for (var i = 0; i < levelConfig.waveConfigurations[waveNumber].numberOfEnemies; i++)
            {
                SpawnEnemy(0);
                yield return new WaitForSeconds(levelConfig.separationTime);
            }

            for (var j = 0; j < levelConfig.waveConfigurations[waveNumber].numberOfFlyEnemies; j++)
            {
                SpawnEnemy(1);
                yield return new WaitForSeconds(levelConfig.separationTime);
            }
        }

        private void SpawnEnemy(int type)
        {
            switch (type)
            {
                case 0:
                    var enemy = Instantiate(enemyPrefab, spanPoint.position, spanPoint.rotation).GetComponent<Enemy>();
                    enemy.OnDefeated += OnEnemyDefeated; // Subscribe to the event
                    enemy.OnNotDefeated += OnEnemyNotDefeated; // Subscribe to the event
                    break;
                case 1:
                    var enemyFly = Instantiate(enemyFlyPrefab, spanPoint.position, spanPoint.rotation)
                        .GetComponent<Enemy>();
                    enemyFly.OnDefeated += OnEnemyDefeated; // Subscribe to the event
                    enemyFly.OnNotDefeated += OnEnemyNotDefeated; // Subscribe to the event
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