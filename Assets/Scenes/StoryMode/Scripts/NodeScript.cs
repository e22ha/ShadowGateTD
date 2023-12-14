// NodeScript.cs

using UnityEngine;

namespace Scenes.StoryMode.Scripts
{
    public class NodeScript : MonoBehaviour
    {
        private NodeBuilder _nodeBuilder;
        
        // public Canvas towerButtonPrefab;

        private void Start()
        {
            _nodeBuilder = NodeBuilder.Instance;
        }

        private void OnMouseDown()
        {
            if (_nodeBuilder == null) return;

            // Check if the clicked node is already selected, if yes, deselect it
            if (_nodeBuilder.IsNodeSelected(gameObject))
            {
                _nodeBuilder.DeselectNode();
                ShowTowerButtons(false);
            }
            else
            {
                _nodeBuilder.SelectNode(gameObject);
                ShowTowerButtons(true);
            }
        }

        private void ShowTowerButtons(bool active)
        {
            _nodeBuilder.menu.SetActive(active);
            // var nodePosition = transform.position;
            // const float yOffset = 1.5f;
            //
            // for (var i = 0; i < _nodeBuilder.prefabList.Count; i++)
            // {
            //     var towerButton = Instantiate(towerButtonPrefab, transform, true);
            //     towerButton.transform.position = nodePosition + new Vector3(0, yOffset * i, 0);
            //     
            //     // Attach the TowerButton script and initialize it with the NodeBuilder and tower index
            //     var towerButtonComponent = towerButton.AddComponent<TowerButton>();
            //     towerButtonComponent.Initialize(_nodeBuilder, i);
            // }
        }
    }
}