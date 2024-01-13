using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.StoryMode.Scripts.Script
{
    public class BuildManger : MonoBehaviour
    {

        //Initialisation of a BuildManger that selects a tower and returns it for construction
    
        public static BuildManger Instance;
        public List<GameObject> prefabList= new();

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one BuildManger in scene!");
                return;
            }
            Instance = this;
        
            prefabList.Add(baseTowerPrefab);
            prefabList.Add(coolTowerPrefab);
            prefabList.Add(plusTowerPrefab);
        }
    
        public GameObject baseTowerPrefab;
        public GameObject coolTowerPrefab;
        public GameObject plusTowerPrefab;

        private GameObject _towerToBuild;

        public GameObject ReturnTowerForBuild()
        {
            return _towerToBuild;
        }

        public void SetTowerToBuild(GameObject tower)
        {
            _towerToBuild = tower;
        }
    }
}
