using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats {
        public int MaxHealth = 100;
        private int _curHealth;
        public int damage = 40;



        public int CurHealth {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, MaxHealth); }
        }

        public void Init() {
            CurHealth = MaxHealth;
        }
    }

    public EnemyStats enemyStats = new EnemyStats();
    public Transform enemyDeathEffect;

    public string audioDeathSoundName = "explosion";

    public float shakeDuration = 1.5f;
    public float shakeAmplitude = 1f;
    public float shakeFrequency = 1f;

    public int moneyDrop = 10;

    [Header("Optional")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start() {
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        enemyStats.Init();
        if (statusIndicator != null) {
            statusIndicator.SetHealth(enemyStats.CurHealth, enemyStats.MaxHealth);
        }
        if (enemyDeathEffect == null) {
            Debug.LogError("No enemyDeathEffect reference found on enemy");
        }
    }

    public void DamageEnemy(int damage) {
        
        enemyStats.CurHealth -= damage;
        if (enemyStats.CurHealth <= 0) {
            //Debug.Log("kill player");
            GameMaster.KillEnemy(this);
        }        
        statusIndicator.SetHealth(enemyStats.CurHealth, enemyStats.MaxHealth);        
    }

    void OnUpgradeMenuToggle(bool active) {
        GetComponent<EnemyAI>().enabled = !active;

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null) {
            _player.DamagePlayer(enemyStats.damage);
            DamageEnemy(9999);

        }
    }

    private void OnDestroy() {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;

    }


}
