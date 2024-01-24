using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManger : MonoBehaviour
{
    private const string DiamondsPlayerPrefsKey = "PlayerDiamonds";

    private static bool _isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        if (_isFirst)
        {
            Debug.Log("DeleteAll");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt(DiamondsPlayerPrefsKey, 5);
            PlayerPrefs.Save();
            _isFirst = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}