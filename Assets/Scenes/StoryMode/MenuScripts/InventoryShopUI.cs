using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.StoryMode.MenuScripts
{
    public class InventoryShopUI : MonoBehaviour
    {
        public InventoryShopManager shopManager;
        public TMP_Text[] itemNameTexts;
        public TMP_Text[] itemCostTexts;

        private void Start()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            for (var i = 0; i < shopManager.shopItems.Count; i++)
            {
                itemNameTexts[i].text = shopManager.shopItems[i].itemName;
                itemCostTexts[i].text = shopManager.shopItems[i].cost.ToString();
            }
        }

        public void OnPurchaseButtonClick(int index)
        {
            shopManager.PurchaseItem(index);
            UpdateUI();
        }  
    }
}