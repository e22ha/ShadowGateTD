using Scenes.StoryMode.Scripts.Script;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    //script for shop tower
    
    private BuildManger _buildManger;

    private void Start()
    {
        _buildManger = BuildManger.Instance;
    }
    
    public void PurchaseTower(int t)
    {
        var towerPrefab = new GameObject();
        switch (t)
        {
            case 1:
                towerPrefab = _buildManger.baseTowerPrefab;
                break;
            case 2:
                towerPrefab = _buildManger.coolTowerPrefab;
                break;
            case 3:
                towerPrefab = _buildManger.plusTowerPrefab;
                break;
        }
        if (PlayerStats.Money >= towerPrefab.GetComponent<Tower>().price)
        {
            Debug.Log(towerPrefab.GetComponent<Tower>().nameTower+" Selected");
            _buildManger.SetTowerToBuild(towerPrefab);
            PlayerStats.UpdateMoney(towerPrefab.GetComponent<Tower>().price);
        }
        else Debug.Log("Not enough money for "+towerPrefab.GetComponent<Tower>().nameTower);
    }
}