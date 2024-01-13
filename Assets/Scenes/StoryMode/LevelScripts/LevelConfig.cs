// LevelConfig.cs
using UnityEngine;

namespace Scenes.StoryMode.LevelScripts
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Game/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [System.Serializable]
        public struct WaveConfiguration
        {
            public int numberOfEnemies;
            public int numberOfFlyEnemies;
        }

        [System.Serializable]
        public struct PathConfiguration
        {
            public Vector3[] pathNodes;
        }

        [System.Serializable]
        public struct TowerPlaceConfiguration
        {
            public Vector3[] towerPlaces;
        }
        
        [System.Serializable]
        public struct NodeConfiguration
        {
            public Node.NodeType nodeType;
            public GameObject prefab;
        }

        public WaveConfiguration[] waveConfigurations;
        public PathConfiguration pathConfiguration;
        public TowerPlaceConfiguration towerPlaceConfiguration;
        public NodeConfiguration[] nodeConfigurations; // Добавляем массив для конфигураций узлов

        public int startMoney = 0;
        public int startLives = 0;
        public int fieldWidth = 8;
        public int fieldHeight = 8;
        public float separationTime = 0.5f;
    }
}