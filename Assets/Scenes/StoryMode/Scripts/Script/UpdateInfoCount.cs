using TMPro;
using UnityEngine;

public class UpdateInfoCount : MonoBehaviour
{
    public TMP_Text lives;
    public TMP_Text money;

    private void Start()
    {
        // Initial update when the script is enabled
        UpdateUI();
    }

    private void FixedUpdate()
    {
        // Update UI in FixedUpdate
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Update lives and money UI
        lives.text = PlayerStats.Lives.ToString("D2");
        money.text = PlayerStats.Money.ToString("D2");
    }
}
