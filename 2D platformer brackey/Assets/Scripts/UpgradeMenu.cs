using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    
    [SerializeField]
    private TMP_Text speedText;

    [SerializeField]
    private float healthMultiplier = 1.2f;
    [SerializeField]
    private float speedMultiplier = 1.1f;

    [SerializeField]
    private int upgradeHealthCost = 100;
    [SerializeField]
    private int upgradeSpeedCost = 50;

    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable() {
        playerStats = PlayerStats.instance;

        UpdateVales();
    }

    // Update is called once per frame
    void UpdateVales()
    {
        healthText.text = "HEALTH: "+playerStats.MaxHealth.ToString();
        speedText.text = "SPEED: "+playerStats.runSpeed.ToString();
    }

    public void UpgradeHealth() {
        if (GameMaster.money < upgradeHealthCost) {
            Debug.Log("Not enough money for health upgrade");
            AudioManager.instance.PlaySound("noMoneyUpgrade");
            return;
        }
        playerStats.MaxHealth = Mathf.RoundToInt(playerStats.MaxHealth * healthMultiplier);
        GameMaster.money -= upgradeHealthCost;
        AudioManager.instance.PlaySound("MoneyUpgradeComplete");
        UpdateVales();
    }

    public void UpgradeSpeed() {
        if (GameMaster.money < upgradeSpeedCost) {
            Debug.Log("Not enough money for speed upgrade");
            AudioManager.instance.PlaySound("noMoneyUpgrade");
            return;
        }
        playerStats.runSpeed = Mathf.Round(playerStats.runSpeed * speedMultiplier);
        GameMaster.money -= upgradeSpeedCost;
        AudioManager.instance.PlaySound("MoneyUpgradeComplete");
        UpdateVales();
    }
}
