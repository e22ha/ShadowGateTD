using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManger : MonoBehaviour
{
    private const string DiamondsPlayerPrefsKey = "PlayerDiamonds";

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(DiamondsPlayerPrefsKey, 5);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
