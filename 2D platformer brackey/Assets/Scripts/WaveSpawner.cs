using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemyPrefab;
        public int count;
        public float rate;
    }

    public Transform[] spawnPoints;

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweemWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start() {
        if (spawnPoints.Length == 0)
            Debug.LogError("No spawn points referenced");

        waveCountdown = timeBetweemWaves;

    }

    // Update is called once per frame
    void Update() {
        if (state == SpawnState.WAITING) {
            // check if enemies are still alive
            if (!EnemyisAlive()) {
                // Begin new round
                WaveCompleted();


            } else {
                return;
            }
        }

        if (waveCountdown <= 0) {
            if (state != SpawnState.SPAWNING) {
                //start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));

            }
        } else {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted() {
        Debug.Log("Wave completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweemWaves;
        nextWave++;
        if (nextWave >= waves.Length) {
            nextWave = 0;
            Debug.Log("All waves completed. Looping.");
        } 

    }

    bool EnemyisAlive() {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0) {
            searchCountdown = 1f;
            //if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }

        
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        state = SpawnState.SPAWNING;
        Debug.Log("Spawning wave: " + _wave.name);
        // Spawn
        for (int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);

                 
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
        Debug.Log("Spawning enemy: " + _enemy.name);

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
    }
}
