using System.Collections.Generic;
using UnityEngine;

public class LevelDescription : MonoBehaviour
{
    //class responsible for level description: description of wave enemies, map size, number of waves
    
    [Header("Desc")]
    
    //x - count of first type enemy; y -- count of second type enemy; z -- time wave
    public List<Vector2Int> listWaveWithCountEnemies; 
    public static List<Vector2Int> ListEnemiesOfWave;
    
    //map size
    public Vector2Int mapSize;
    public static Vector2Int MapSize;
    
    //count of wave
    public static int WaveCount;
    
    private void Awake()
    {
        ListEnemiesOfWave = listWaveWithCountEnemies;
        MapSize = mapSize;
        WaveCount = listWaveWithCountEnemies.Count;
    }
}
