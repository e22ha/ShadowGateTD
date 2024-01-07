// Node.cs

using UnityEngine;

namespace Scenes.StoryMode.LevelScripts
{
    public class Node : MonoBehaviour
    {
        public NodeType nodeType;

        public enum NodeType
        {
            Path,
            Tower,
            Empty
        }

        // Reference to the UI canvas prefab
        public GameObject towerUIPrefab;

        private Material originalMaterial;
        private TowerDefenseField _towerDefenseField;
        private bool isSelected = false;

        private void Start()
        {
            originalMaterial = GetComponent<Renderer>().material;
            // Set the initial node type based on your logic
            SetNodeType(nodeType);
        }

        public void SetNodeType(NodeType type)
        {
            nodeType = type;
            // Set visuals based on the node type
            // You can customize this based on your game's visual representation logic
            switch (nodeType)
            {
                case NodeType.Path:
                    // Set visuals for a path node
                    break;
                case NodeType.Tower:
                    // Set visuals for a tower node
                    // For example, you might change the color or add a tower model
                    break;
                case NodeType.Empty:
                    // Set visuals for an empty node
                    break;
            }
        }

        public void SetTowerDefenseField(TowerDefenseField field)
        {
            _towerDefenseField = field;
        }

        private void OnMouseDown()
        {
            // Handle node click
            if (_towerDefenseField != null)
            {
                _towerDefenseField.OnNodeClick(this);
            }
            else
            {
                Debug.Log("towerDefenseField = null");
            }
        }

        public void ShowTowerUI()
        {
            // Instantiate the tower UI prefab and attach it to the node
            GameObject towerUI = Instantiate(towerUIPrefab, transform.position, Quaternion.identity);
            towerUI.transform.parent = transform;

            // Get the TowerUIButton script from the UI prefab
            TowerUIButton towerUIButton = towerUI.GetComponent<TowerUIButton>();
            if (towerUIButton != null)
            {
                // Set the node reference in the button script
                towerUIButton.SetNodeReference(this);
            }

            // Mark the node as selected
            isSelected = true;
        }

        public void HighlightNode(Material highlightMaterial)
        {
            // Highlight the node with a material
            var nodeRenderer = GetComponent<Renderer>();
            if (nodeRenderer != null)
            {
                nodeRenderer.material = highlightMaterial;
            }
        }

        public void Deselect()
        {
            // Reset the node highlight
            var nodeRenderer = GetComponent<Renderer>();
            if (nodeRenderer != null)
            {
                nodeRenderer.material = originalMaterial;
            }

            // Remove the tower UI if it exists
            var existingUI = transform.Find("TowerUI");
            if (existingUI != null)
            {
                Destroy(existingUI.gameObject);
            }

            // Mark the node as deselected
            isSelected = false;
        }
    }
}