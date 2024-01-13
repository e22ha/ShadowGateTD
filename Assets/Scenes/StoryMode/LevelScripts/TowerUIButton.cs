using System;
using Scenes.StoryMode.LevelScripts.Script;
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

        private Shop _shop;
        
        private void Start()
        {
            _shop = Shop.Instance;
        }

        public void OnButton1Click()
        {
            _shop.PurchaseTower(1, _node);
        }

        public void OnButton2Click()
        {
            _shop.PurchaseTower(2, _node);
        }

        public void OnButton3Click()
        {
            _shop.PurchaseTower(3, _node);
        }
    }
}