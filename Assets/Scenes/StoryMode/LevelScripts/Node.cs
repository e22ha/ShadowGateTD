// Node.cs

using Scenes.StoryMode.Scripts;
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
        public TowerUIButton towerUI;

        private Material _originalMaterial;
        private TowerDefenseField _towerDefenseField;
        private bool _isSelected = false;
        private NodeOccupancy _nodeOccupancy;

        private void Start()
        {
            _originalMaterial = GetComponent<Renderer>().material;
            // Set the initial node type based on your logic
            SetNodeType(nodeType);
            _nodeOccupancy = GetComponent<NodeOccupancy>();
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
                    towerUI.SetNodeReference(this);
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
            if(nodeType != NodeType.Tower) return;
            if (_nodeOccupancy.IsOccupied) return;
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
            towerUI.gameObject.SetActive(true);
            // Mark the node as selected
            _isSelected = true;
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
                nodeRenderer.material = _originalMaterial;
            }

            towerUI.gameObject.SetActive(false);

            // Mark the node as deselected
            _isSelected = false;
        }
    }
}