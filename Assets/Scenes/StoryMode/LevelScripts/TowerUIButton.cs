using UnityEngine;

namespace Scenes.StoryMode.LevelScripts
{
    public class TowerUIButton : MonoBehaviour
    {
        private Node _node;

        public void SetNodeReference(Node targetNode)
        {
            _node = targetNode;
        }

        public void OnButton1Click()
        {
            Debug.Log("Button 1 Clicked on Node: " + _node.gameObject.name);
            // Add your logic for button 1 click
        }

        public void OnButton2Click()
        {
            Debug.Log("Button 2 Clicked on Node: " + _node.gameObject.name);
            // Add your logic for button 2 click
        }

        public void OnButton3Click()
        {
            Debug.Log("Button 3 Clicked on Node: " + _node.gameObject.name);
            // Add your logic for button 3 click
        }
    }
}