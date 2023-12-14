using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.StoryMode.Scripts
{
    public class WaveSpawner : MonoBehaviour
    {
        public Transform enemyPrefab;
        public Transform enemyFlyPrefab;
        public Transform spanPoint;
    
        private float _countdown = 2f;

        private int _waveNumber;
        private int _maxWaveNumber;

        [Header("UI")]
        public TMP_Text waveBarText;
    
        public GameObject pauseButton;

        public LevelConfig levelConfig;  // Reference to the level configuration asset

        private void Start()
        {
            Debug.Log($"{name} is Awake!");
            _maxWaveNumber = levelConfig.waveConfigurations.Length;
            ControlWaveBar();
            Time.timeScale = 1f;
        
            PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
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
                // GameStatusControl.WaveIsOver = true;
            }
        
            _countdown -= Time.deltaTime;
        }
    

        public void SkipTime()
        {
            _countdown = 0f;
        }

        public void Pause()
        {
            Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
        }

        public void TimeMultiplier()
        {
            Time.timeScale = 1f;
            Time.timeScale *= (Time.timeScale == 1f) ? 2f : 1f;
        }

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
                    Instantiate(enemyPrefab, spanPoint.position, spanPoint.rotation);
                    break;
                case 1:
                    Instantiate(enemyFlyPrefab, spanPoint.position, spanPoint.rotation);
                    break;
            }

            // GameStatusControl.CountEnemies++;
        }
    }
}