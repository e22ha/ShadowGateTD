using System.Collections.Generic;
using Scenes.StoryMode.LevelScripts.Script;
using Scenes.StoryMode.Scripts;
using Scenes.StoryMode.Scripts.Script;
using UnityEngine;

namespace Scenes.StoryMode.LevelScripts
{
    public class Shop : MonoBehaviour
    {
        
        public static Shop Instance { get; set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // Ensures only one instance exists
            }
        }
    
        public GameObject baseTowerPrefab;
        public GameObject coolTowerPrefab;
        public GameObject plusTowerPrefab;

        private GameObject _towerToBuild;
        
    
        public void PurchaseTower(int t, Node selectedNode)
        {
            var towerPrefab = new GameObject();
            switch (t)
            {
                case 1:
                    towerPrefab = baseTowerPrefab;
                    break;
                case 2:
                    towerPrefab = coolTowerPrefab;
                    break;
                case 3:
                    towerPrefab = plusTowerPrefab;
                    break;
            }
            if (selectedNode != null && towerPrefab != null)
            {
                var nodeOccupancy = selectedNode.GetComponent<NodeOccupancy>();

                if (!nodeOccupancy.IsOccupied)
                {
                    var towerPrice = towerPrefab.GetComponent<Tower>().price;

                    if (PlayerStats.CanAfford(towerPrice))
                    {
                        PlayerStats.UpdateMoney(-towerPrice);
                        Instantiate(towerPrefab,
                            selectedNode.transform.position + Vector3.up * nodeOccupancy.yOffset,
                            selectedNode.transform.rotation);
                        nodeOccupancy.MarkOccupied();
                        selectedNode.Deselect();
                    }
                    else
                    {
                        Debug.LogWarning("Not enough money to build this tower!");
                    }
                }
                else
                {
                    Debug.LogWarning("Node is already occupied!");
                }
            }
            else
            {
                Debug.LogWarning("No node or tower prefab selected!");
            }
        }
    }
}