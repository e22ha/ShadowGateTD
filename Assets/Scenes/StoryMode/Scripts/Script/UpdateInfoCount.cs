using TMPro;
using UnityEngine;

public class UpdateInfoCount : MonoBehaviour
{
    //script for update GameInfo
    
    public TMP_Text lives;
    public TMP_Text livesShadow;
    
    public TMP_Text money;
    public TMP_Text moneyShadow;

    public TMP_Text score;
    public TMP_Text scoreShadow;
    
    private void FixedUpdate()
    {
        lives.text = PlayerStats.Lives.ToString("D2");
        livesShadow.text = PlayerStats.Lives.ToString("D2");
        
        money.text = PlayerStats.Money.ToString("D2");
        moneyShadow.text = PlayerStats.Money.ToString("D2");
    
        score.text = PlayerStats.Score.ToString("D3");
        scoreShadow.text = PlayerStats.Score.ToString("D3");
    }
}
