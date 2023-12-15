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
            }
            else
            {
                _nodeBuilder.SelectNode(gameObject);
            }
        }

        
    }
}