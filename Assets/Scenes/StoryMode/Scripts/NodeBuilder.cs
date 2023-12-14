using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.StoryMode.Scripts
{
    public class NodeBuilder : MonoBehaviour
    {
        // Static reference to the NodeBuilder instance
        public static NodeBuilder Instance;

        public Material highlightMaterial;

        // List of tower prefabs
        public List<GameObject> prefabList = new List<GameObject>();

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one NodeBuilder in the scene!");
                return;
            }
            Instance = this;

            // Add your tower prefabs to the list
            prefabList.Add(baseTowerPrefab);
            prefabList.Add(coolTowerPrefab);
            prefabList.Add(plusTowerPrefab);
        }

        public GameObject baseTowerPrefab;
        public GameObject coolTowerPrefab;
        public GameObject plusTowerPrefab;
        
        // Reference to the selected node
        private GameObject _selectedNode;

        // Reference to the selected tower prefab
        private GameObject _selectedTowerPrefab;

        public void SelectNode(GameObject node)
        {
            ResetSelectedNode();
            HighlightNode(node);
            _selectedNode = node;
        }

        public string GetTowerName(int towerIndex)
        {
            if (towerIndex >= 0 && towerIndex < prefabList.Count)
            {
                // Assuming each tower prefab has a Tower script attached
                var towerScript = prefabList[towerIndex].GetComponent<Tower>();

                if (towerScript != null)
                {
                    return towerScript.nameTower;
                }
                else
                {
                    Debug.LogError($"Tower script not found on the prefab at index {towerIndex}.");
                }
            }
            else
            {
                Debug.LogError("Invalid tower index or prefab list is not set up correctly.");
            }

            return "Unknown Tower";
        }
        public void SelectTowerPrefab(int towerIndex)
        {
            if (towerIndex >= 0 && towerIndex < prefabList.Count)
            {
                _selectedTowerPrefab = prefabList[towerIndex];
            }
            else
            {
                Debug.LogError("Invalid tower index or prefab list is not set up correctly.");
            }
        }

        public void BuildTower()
        {
            if (_selectedNode != null && _selectedTowerPrefab != null)
            {
                var nodeOccupancy = _selectedNode.GetComponent<NodeOccupancy>();

                if (!nodeOccupancy.IsOccupied)
                {
                    var towerPrice = _selectedTowerPrefab.GetComponent<Tower>().price;

                    if (PlayerStats.CanAfford(towerPrice))
                    {
                        PlayerStats.UpdateMoney(-towerPrice);
                        Instantiate(_selectedTowerPrefab, _selectedNode.transform.position + Vector3.up * nodeOccupancy.yOffset, _selectedNode.transform.rotation);
                        nodeOccupancy.MarkOccupied();
                        ResetSelectedNode();
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

        private void HighlightNode(GameObject node)
        {
            // Assuming you have a material to use for highlighting

            if (node != null)
            {
                var nodeRenderer = node.GetComponent<Renderer>();

                if (nodeRenderer != null)
                {
                    var originalMaterial = nodeRenderer.material;
                    nodeRenderer.material = highlightMaterial;

                    // Optionally, you can use a coroutine to revert the material after a certain time
                    // StartCoroutine(RevertMaterialAfterDelay(nodeRenderer, originalMaterial, /* your delay */));
                }
                else
                {
                    Debug.LogWarning("Node does not have a Renderer component.");
                }
            }
            else
            {
                Debug.LogWarning("Node is null.");
            }
        }

        public Material HighlightMat { get; set; }

        private void ResetSelectedNode()
        {
            if (_selectedNode != null)
            {
                // Implement reset logic (e.g., revert color, scale)
            }

            _selectedNode = null;
            _selectedTowerPrefab = null;
        }
    }
}
