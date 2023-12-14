using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManger : MonoBehaviour
{

    //Initialisation of a BuildManger that selects a tower and returns it for construction
    
    public static BuildManger Instance;
    public List<GameObject> prefubList= new();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildManger in scene!");
            return;
        }
        Instance = this;
        
        prefubList.Add(baseTowerPrefab);
        prefubList.Add(coolTowerPrefab);
        prefubList.Add(plusTowerPrefab);
    }
    
    public GameObject baseTowerPrefab;
    public GameObject coolTowerPrefab;
    public GameObject plusTowerPrefab;

    private GameObject _towerToBuild;
    public Material highlightMaterial;

    public GameObject ReturnTowerForBuild()
    {
        return _towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        _towerToBuild = tower;
    }
}
