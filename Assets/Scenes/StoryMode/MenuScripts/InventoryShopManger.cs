namespace Scenes.StoryMode.MenuScripts
{
// ShopManager.cs
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryShopManager : MonoBehaviour
    {
        public List<InventoryShopItem> shopItems;

        private void Start()
        {
            // Инициализация магазина (может быть загрузка из файла сохранения)
            InitializeShop();
        }

        private void InitializeShop()
        {
            // Логика загрузки данных магазина из PlayerPrefs
            for (var i = 0; i < shopItems.Count; i++)
            {
                // По умолчанию все товары не куплены
                shopItems[i].isPurchased = PlayerPrefs.GetInt("ShopItem_" + i, 0) == 1;
            }
        }

        public void PurchaseItem(int index)
        {
            // Покупка товара по индексу
            if (index >= 0 && index < shopItems.Count)
            {
                if (!shopItems[index].isPurchased)
                {
                    // Здесь добавьте логику для списывания кристаллов и активации товара
                    // Например, уменьшите количество кристаллов игрока
                    // и активируйте соответствующую суперсилу
                    shopItems[index].isPurchased = true;

                    // Сохранение изменений (может потребоваться в зависимости от вашей реализации)
                    SaveShopData(index);
                }
                else
                {
                    Debug.Log("Этот товар уже куплен.");
                }
            }
            else
            {
                Debug.LogError("Некорректный индекс товара.");
            }
        }

        void SaveShopData(int index)
        {
            // Сохранение информации о покупке в PlayerPrefs
            PlayerPrefs.SetInt("ShopItem_" + index, 1);
            PlayerPrefs.Save();
        }
    }

}