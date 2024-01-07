// TowerDefenseField.cs
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.StoryMode.LevelScripts
{
    public class TowerDefenseField : MonoBehaviour
    {
        public LevelConfig levelConfig;
        private List<List<Node>> nodeGrid = new List<List<Node>>();

        void Start()
        {
            CreateNodeGrid(levelConfig.fieldWidth, levelConfig.fieldHeight);
        }

        void CreateNodeGrid(int width, int height)
        {
            float spacing = 0.8f;

            for (int i = 0; i < height; i++)
            {
                List<Node> row = new List<Node>();
                for (int j = 0; j < width; j++)
                {
                    Vector3 nodePosition = new Vector3(j * spacing, 0f, i * spacing);

                    // Определите тип нода на основе конфигурации
                    Node.NodeType nodeType = DetermineNodeType(j, i);

                    // Получите префаб для этого типа нода
                    GameObject nodePrefab = GetNodePrefabForType(nodeType);

                    // Создайте нод с использованием префаба
                    GameObject nodeGameObject = Instantiate(nodePrefab, nodePosition, Quaternion.identity);
                    Node node = nodeGameObject.GetComponentInChildren<Node>();

                    // Установите начальный тип нода в соответствии с вашей логикой
                    node.SetNodeType(nodeType);

                    // Установите ссылку на TowerDefenseField
                    node.SetTowerDefenseField(this);

                    // Добавьте нод в список ряда
                    row.Add(node);
                }

                // Добавьте ряд в сетку нодов
                nodeGrid.Add(row);
            }
        }

        Node.NodeType DetermineNodeType(int column, int row)
        {
            // Реализуйте логику определения типа нода на основе конфигурации
            // и укажите также позицию для каждого нода.
            // Например, используйте данные из levelConfig.nodeConfigurations
            // в зависимости от значения row и column.
            
            // Пример: предположим, что у вас есть конфигурация для путей:
            if (IsInPath(column, row))
            {
                return Node.NodeType.Path;
            }
            
            // Пример: предположим, что у вас есть конфигурация для мест размещения башен:
            if (IsInTowerPlace(column, row))
            {
                return Node.NodeType.Tower;
            }

            // По умолчанию возвращайте тип Empty
            return Node.NodeType.Empty;
        }

        bool IsInPath(int column, int row)
        {
            // Проверьте, является ли данный узел частью пути
            foreach (Vector3 pathNode in levelConfig.pathConfiguration.pathNodes)
            {
                if (Mathf.RoundToInt(pathNode.x) == column && Mathf.RoundToInt(pathNode.z) == row)
                {
                    return true;
                }
            }
            return false;
        }

        bool IsInTowerPlace(int column, int row)
        {
            // Проверьте, является ли данный узел местом размещения башен
            foreach (Vector3 towerPlace in levelConfig.towerPlaceConfiguration.towerPlaces)
            {
                if (Mathf.RoundToInt(towerPlace.x) == column && Mathf.RoundToInt(towerPlace.z) == row)
                {
                    return true;
                }
            }
            return false;
        }

        GameObject GetNodePrefabForType(Node.NodeType type)
        {
            // Верните префаб, соответствующий указанному типу нода
            foreach (var config in levelConfig.nodeConfigurations)
            {
                if (config.nodeType == type)
                {
                    return config.prefab;
                }
            }

            // Если не найден префаб, верните null
            return null;
        }
        
        public Material highlightMaterial;
        private Node _selectedNode;

        public void HighlightNode(Node node)
        {
            // Highlight the specified node
            node.HighlightNode(highlightMaterial);
        }

        public void ShowTowerUI(Node node)
        {
            // Show UI for the specified node
            node.ShowTowerUI();
        }


        public void DeselectNode()
        {
            // Deselect the currently selected node (if any)
            if (_selectedNode != null)
            {
                _selectedNode.Deselect();
                _selectedNode = null;
            }
        }

        public void OnNodeClick(Node node)
        {
            // Handle node click
            if (node.nodeType == Node.NodeType.Tower)
            {
                if (_selectedNode == node)
                {
                    // Deselect if the same node is clicked again
                    DeselectNode();
                }
                else
                {
                    // Deselect the currently selected node (if any)
                    DeselectNode();

                    // Show UI and highlight the clicked node
                    ShowTowerUI(node);
                    HighlightNode(node);
                    _selectedNode = node;
                }
            }
        }
    }
}
