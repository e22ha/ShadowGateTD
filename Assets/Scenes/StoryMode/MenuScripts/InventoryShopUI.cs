using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scenes.StoryMode.MenuScripts
{
    public class InventoryShopUI : MonoBehaviour
    {
        public InventoryShopManager shopManager;
        public TMP_Text[] itemNameTexts;
        public TMP_Text[] itemCostTexts;
        
        public TMP_Text diamondsCountText;
        

        public InventoryShopConfig shopConfig;

        private void Start()
        {
            UpdateUI();
        }
        private const string DiamondsPlayerPrefsKey = "PlayerDiamonds";

        
        private static int GetPlayerDiamonds()
        {
            return PlayerPrefs.GetInt(DiamondsPlayerPrefsKey, 5);
        }

        private void UpdateUI()
        {
            diamondsCountText.text = GetPlayerDiamonds().ToString();
            for (var i = 0; i < shopConfig.shopItems.Count && i < itemNameTexts.Length && i < itemCostTexts.Length; i++)
            {
                itemNameTexts[i].text = shopConfig.shopItems[i].itemName;
                itemCostTexts[i].text = shopConfig.shopItems[i].cost.ToString();
            }
        }

        public void OnPurchaseButtonClick(int index)
        {
            if (index >= 0 && index < shopConfig.shopItems.Count)
            {
                shopManager.PurchaseItem(index);
                UpdateUI();
            }
            else
            {
                Debug.LogError("Invalid item index.");
            }
        }
    }
}