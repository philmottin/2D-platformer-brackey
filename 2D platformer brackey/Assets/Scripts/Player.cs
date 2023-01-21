using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int MaxHealth = 100;
        private int _curHealth;
        public int CurHealth {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, MaxHealth); }
        }

        public void Init() {
            CurHealth = MaxHealth;
        }
    }

    public PlayerStats playerStats = new PlayerStats();
    public int fallBoundary = -20;
        
    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start() {
        playerStats.Init();
        if (statusIndicator != null) {
            statusIndicator.SetHealth(playerStats.CurHealth, playerStats.MaxHealth);
        } else {
            Debug.LogError("No StatusIndicator reference found on player");
        }
    }

    public void DamagePlayer(int damage) {
        playerStats.CurHealth -= damage;
        if (playerStats.CurHealth <= 0) {
            GameMaster.KillPlayer(this);
        }
        statusIndicator.SetHealth(playerStats.CurHealth, playerStats.MaxHealth);
    }

    private void Update() {
        if(transform.position.y <= fallBoundary) {
            DamagePlayer(9999);

        }
    }

}
