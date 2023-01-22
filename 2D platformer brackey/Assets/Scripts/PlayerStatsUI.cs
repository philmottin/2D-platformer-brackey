using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text livesText;
    [SerializeField]
    TMP_Text moneyCounter;
    [SerializeField]
    TMP_Text moneyCounterShop;
    // Start is called before the first frame update
    void Start()
    {
        if (livesText == null) {
            Debug.LogError("No livesText referenced");
            this.enabled = false;
        }
        if (moneyCounter == null) {
            Debug.LogError("No moneyCounter referenced");
            this.enabled = false;
        }
        if (moneyCounter == null) {
            Debug.LogError("No moneyCounterShop referenced");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "LIVES: "+GameMaster.RemainingLives.ToString();
        moneyCounter.text = "$ "+GameMaster.money.ToString();
        moneyCounterShop.text = "$ "+GameMaster.money.ToString();
    }
}
