using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStars : MonoBehaviour
{
    public GameObject[] stars;

    public Sprite starSprite;

    public void Update()
    {
        int starsNum = PlayerPrefs.GetInt("StarsTemp");

        if (starsNum > 0)
        {
            for (int i = 0; i < starsNum; i++)
            {
                stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
            }
        }
    }
}