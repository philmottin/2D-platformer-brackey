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

    [SerializeField]
    private int startingMoney;

    public static int money;

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
    [SerializeField]
    private GameObject upgradeUI;

    [SerializeField]
    private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    public CinemachineShake_coroutine cinemachineShake;

    //cache
    private AudioManager audioManager;

    private void Start() {
        if (cinemachineShake == null) {
            Debug.LogError("No cinemachineShake reference found in GameMaster");
        }

        _remainingLives = maxLives;
        money = startingMoney;

        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.Log("No AudioManager found in the scene");
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            ToggleUpgradeMenu();
        }
    }

    public void EndGame() {
        //Debug.Log("GAME OVER");
        audioManager.PlaySound("gameover");

        gameOverUI.SetActive(true);
    }

    public void ToggleUpgradeMenu() {
        //Debug.Log("upgrade menu");

        upgradeUI.SetActive(!upgradeUI.activeSelf);
        waveSpawner.enabled = !upgradeUI.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeUI.activeSelf);
    }

    public IEnumerator RespawnPlayer() {
        //GetComponent<AudioSource>().Play();
        audioManager.PlaySound("Respawn");

        yield return new WaitForSeconds(spawnDelay);
        audioManager.PlaySound("RespawnPlayer");
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
        audioManager.PlaySound(_enemy.audioDeathSoundName);

        money += _enemy.moneyDrop;

        // Destroy on the particle system
        Instantiate(_enemy.enemyDeathEffect, _enemy.transform.position, Quaternion.identity);
        cinemachineShake.Shake(_enemy.shakeDuration, _enemy.shakeAmplitude, _enemy.shakeFrequency);
        Destroy(_enemy.gameObject);
        
    }

}
