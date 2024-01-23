using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Scenes.StoryMode.MenuScripts
{
    public class InventoryShopManager : MonoBehaviour
    {
        private const string PlayerPrefsKey = "ShopItems";

        public InventoryShopConfig shopConfig;
        
        private const string DiamondsPlayerPrefsKey = "PlayerDiamonds";

        private void Awake()
        {
            //remove in build, this is need to check shop function
            // PlayerPrefs.SetInt(DiamondsPlayerPrefsKey, 3);
            Debug.Log($"Count of diamonds - {GetPlayerDiamonds()}");
        }


        // Add this method to your InventoryShopManager
        private static int GetPlayerDiamonds()
        {
            return PlayerPrefs.GetInt(DiamondsPlayerPrefsKey, 5);
        }

        private static void SetPlayerDiamonds(int diamonds)
        {
            PlayerPrefs.SetInt(DiamondsPlayerPrefsKey, PlayerPrefs.GetInt(DiamondsPlayerPrefsKey) + diamonds);
            PlayerPrefs.Save();
        }


        private void SaveShopData(int index)
        {
            // Получаем значение isPurchased для только что купленной фичи
            var isPurchasedValue = shopConfig.shopItems[index].isPurchased;

            // Создаем уникальный ключ для PlayerPrefs, например, используя индекс
            var key = PlayerPrefsKey + "_" + index;


            // Сохраняем только что купленную фичу в PlayerPrefs
            PlayerPrefs.SetInt(key, isPurchasedValue ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void PurchaseItem(int index)
        {
            if (index >= 0 && index < shopConfig.shopItems.Count)
            {
                if (!shopConfig.shopItems[index].isPurchased)
                {
                    if (GetPlayerDiamonds() >= shopConfig.shopItems[index].cost)
                    {
                        shopConfig.shopItems[index].isPurchased = true;

                        SetPlayerDiamonds(-shopConfig.shopItems[index].cost);

                        Debug.Log($"Items '{shopConfig.shopItems[index].itemName}' is purchased");
                        // Передаем индекс в SaveShopData
                        SaveShopData(index);
                    }
                    else
                    {
                        Debug.Log("Not enough diamonds to purchase this item.");
                    }
                }
                else
                {
                    Debug.Log("This item is already purchased.");
                }
            }
            else
            {
                Debug.LogError("Invalid item index.");
            }
        }
    }
}