using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats {
        public int Health = 100;
    }

    public EnemyStats playerStats = new EnemyStats();

    public void DamageEnemy(int damage) {
        playerStats.Health -= damage;
        if (playerStats.Health <= 0) {
            //Debug.Log("kill player");
            GameMaster.KillEnemy(this);
        }
    }

    
}
