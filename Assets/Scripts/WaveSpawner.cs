using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform enemyFlyPrefab;
    public Transform spanPoint;
    
    private float _timeBetweenWave;
    private float _countdown = 2f;
    private float _timeMultiplier = 1f;

    private int _waveNumber = 0;
    private int _maxWaveNumber = 0;
    public float sepTime = 0.5f;

    [Header("UI")]
    public TMP_Text time;
    public TMP_Text timeShadow;

    public TMP_Text waveBarText;
    
    public GameObject pauseButton;

    private readonly List<Vector2Int> _waveDesc = new List<Vector2Int>();

    private void Start()
    {
        Debug.Log($"{name} is Awake!");
        _waveDesc.AddRange(LevelDescription.ListEnemiesOfWave);
        Debug.Log($"Count wave - {_waveDesc.Count}");
        _maxWaveNumber = _waveDesc.Count;
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
        time.text = Mathf.Floor(_countdown + 1f).ToString();
        timeShadow.text = Mathf.Floor(_countdown + 1f).ToString();
        if(_waveNumber < _maxWaveNumber)
        {
            if (_countdown <= 0f) 
            {
                _countdown = _waveDesc[_waveNumber][0]+_waveDesc[_waveNumber][1];
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
        Time.timeScale = Time.timeScale >= 1 ? 0 : 1 * _timeMultiplier;
    }

    public void TimeMultiplier()
    {
        Time.timeScale = 1f;
        _timeMultiplier = _timeMultiplier switch
        {
            1f => 2f,
            _ => 1f
        };
        Time.timeScale *= _timeMultiplier;
    }

    private IEnumerator SpawnWave(int waveNumber)
    {
        for (var i = 0; i < _waveDesc[waveNumber].x; i++)
        {
            SpawnEnemy(0);
            yield return new WaitForSeconds(sepTime);
        }
        for (var j = 0; j < _waveDesc[waveNumber].y; j++)
        {
            SpawnEnemy(1);
            yield return new WaitForSeconds(sepTime);
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