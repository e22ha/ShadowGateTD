using UnityEngine;

public class NodeScript : MonoBehaviour
{
    
    //class of the node for controlling the mouse enter
    
    public float yOffset;
    
    private GameObject _tower;

    private Renderer _render;

    //private BuildManger _buildManger;

    private void Start()
    {
        _render = GetComponent<Renderer>();

        //_buildManger = BuildManger.Instance;
    }


    private void OnMouseDown()
    {
        // if (_buildManger.ReturnTowerForBuild() == null || _tower != null)
        // {
        //     Debug.LogWarning("Busy or buildForTower = null");
        //     return;
        // }

        //build tower
        // var towerBuild = _buildManger.ReturnTowerForBuild();
        //
        // var price = towerBuild.GetComponent<Tower>().price;
        //
        // PlayerStats.UpdateMoney(price);

        
        // _tower = Instantiate(towerBuild, transform.position + Vector3.up * yOffset, transform.rotation);
        Debug.Log("Tower is build");
    }


    private void OnMouseEnter()
    {
        // if (_buildManger.ReturnTowerForBuild() == null) return;
    }
    
}