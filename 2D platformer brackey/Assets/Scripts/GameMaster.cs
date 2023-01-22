using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public CinemachineVirtualCamera cinemachine;

    [SerializeField]
    private int maxLives = 3;

    private static int _remainingLives;
    public static int RemainingLives { get { return _remainingLives; } }

    private void Awake() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        
    }


    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 3.5f;
    public Transform spawnEffectPrefab;

    [SerializeField]
    private GameObject gameOverUI;

    public CinemachineShake_coroutine cinemachineShake;

    private void Start() {
        if (cinemachineShake == null) {
            Debug.LogError("No cinemachineShake reference found in GameMaster");
        }

        _remainingLives = maxLives;
    }

    public void EndGame() {
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer() {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        Transform clone =  Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        cinemachine.Follow = clone;
        Transform cloneParticles =  Instantiate(spawnEffectPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(cloneParticles.gameObject, 3f);

    }
    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        _remainingLives--;
        if (_remainingLives <=0) {
            gm.EndGame();
            
        } else {
            gm.StartCoroutine(gm.RespawnPlayer());
        }

    }

    public static void KillEnemy(Enemy enemy) {
        gm._killEnemy(enemy);
    }

    public void _killEnemy(Enemy _enemy) {
        // Destroy on the particle system
        Instantiate(_enemy.enemyDeathEffect, _enemy.transform.position, Quaternion.identity);
        cinemachineShake.Shake(_enemy.enemyStats.shakeDuration, _enemy.enemyStats.shakeAmplitude, _enemy.enemyStats.shakeFrequency);
        Destroy(_enemy.gameObject);
        
    }

}
