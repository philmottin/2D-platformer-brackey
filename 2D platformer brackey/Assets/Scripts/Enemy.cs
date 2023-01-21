using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats {
        public int MaxHealth = 100;
        private int _curHealth;
        public int CurHealth
        {
            get { return _curHealth; }
            set
            {
                _curHealth = Mathf.Clamp(value, 0, MaxHealth);
            }
        }

        public void Init() {
            CurHealth = MaxHealth;
        }
    }

    public EnemyStats enemyStats = new EnemyStats();

    [Header("Optional")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start() {
        enemyStats.Init();
        if (statusIndicator != null) {
            statusIndicator.SetHealth(enemyStats.CurHealth, enemyStats.MaxHealth);
        }
    }

    public void DamageEnemy(int damage) {
        
        enemyStats.CurHealth -= damage;
        if (enemyStats.CurHealth <= 0) {
            //Debug.Log("kill player");
            GameMaster.KillEnemy(this);
        }

        if (statusIndicator != null) {
            statusIndicator.SetHealth(enemyStats.CurHealth, enemyStats.MaxHealth);
        }
    }

    
}
