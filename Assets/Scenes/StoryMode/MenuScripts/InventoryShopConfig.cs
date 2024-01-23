using System.Collections.Generic;
using UnityEngine;
namespace Scenes.StoryMode.MenuScripts
{

    [CreateAssetMenu(fileName = "InventoryShopConfig", menuName = "Config/InventoryShopConfig", order = 1)]
    public class InventoryShopConfig : ScriptableObject
    {
        [System.Serializable]
        public class ShopItem
        {
            public string itemName;
            public int cost;
            public bool isPurchased = false;
        }

        public List<ShopItem> shopItems = new List<ShopItem>();
    }
}