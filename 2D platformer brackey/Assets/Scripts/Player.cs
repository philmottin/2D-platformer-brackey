using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPlayerMovement))]
public class Player : MonoBehaviour
{
    

    //public PlayerStats playerStats = new PlayerStats();
    public int fallBoundary = -20;
        
    [SerializeField]
    private StatusIndicator statusIndicator;


    private PlayerStats playerStats;

    //cache
    private AudioManager audioManager;



    private void Start() {

        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        playerStats = PlayerStats.instance;
        playerStats.CurHealth = playerStats.MaxHealth;

        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.Log("No AudioManager found in the scene");
        }

        //playerStats.Init();
        if (statusIndicator != null) {
            statusIndicator.SetHealth(playerStats.CurHealth, playerStats.MaxHealth);
        } else {
            Debug.LogError("No StatusIndicator reference found on player");
        }

        InvokeRepeating("RegenHealth", 1/playerStats.healthRegenRate, 1/playerStats.healthRegenRate);
    }
    private void Update() {
        if(transform.position.y <= fallBoundary) {
            DamagePlayer(9999);

        }
    }

    void RegenHealth() {
        playerStats.CurHealth += 1;
        statusIndicator.SetHealth(playerStats.CurHealth, playerStats.MaxHealth);

    }


    void OnUpgradeMenuToggle(bool active) {
        GetComponent<MyPlayerMovement>().enabled = !active;
        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null) {
            _weapon.enabled = !active;
        }

    }

    public void DamagePlayer(int damage) {
        playerStats.CurHealth -= damage;
        if (playerStats.CurHealth <= 0) {
            audioManager.PlaySound("deathPlayer");
            GameMaster.KillPlayer(this);
        } else {
            audioManager.PlaySound("damagePlayer");
        }
        statusIndicator.SetHealth(playerStats.CurHealth, playerStats.MaxHealth);
    }

    private void OnDestroy() {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;

    }


}
