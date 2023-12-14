// NodeScript.cs

using UnityEngine;

namespace Scenes.StoryMode.Scripts
{
    public class NodeScript : MonoBehaviour
    {
        private NodeBuilder _nodeBuilder;

        public GameObject towerButtonPrefab;

        private void Start()
        {
            _nodeBuilder = NodeBuilder.Instance;
        }

        private void OnMouseDown()
        {
            if (_nodeBuilder == null) return;
            _nodeBuilder.SelectNode(gameObject);
            ShowTowerButtons();
        }

        private void ShowTowerButtons()
        {
            var nodePosition = transform.position;
            const float yOffset = 1.5f;

            for (var i = 0; i < _nodeBuilder.prefabList.Count; i++)
            {
                var towerButton = Instantiate(towerButtonPrefab, transform, true);
                towerButton.transform.position = nodePosition + new Vector3(0, yOffset * i, 0);
                
                // Attach the TowerButton script and initialize it with the NodeBuilder and tower index
                var towerButtonComponent = towerButton.AddComponent<TowerButton>();
                towerButtonComponent.Initialize(_nodeBuilder, i);
            }
        }
    }
}