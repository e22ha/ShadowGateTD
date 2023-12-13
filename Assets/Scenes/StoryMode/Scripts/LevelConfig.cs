using UnityEngine;

namespace Scenes.StoryMode.Scripts
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

        public WaveConfiguration[] waveConfigurations;
        public float separationTime = 0.5f;
    }
}