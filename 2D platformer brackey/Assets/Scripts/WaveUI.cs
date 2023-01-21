using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    TMP_Text waveCountdownText;
    [SerializeField]
    TMP_Text waveCountText;

    private WaveSpawner.SpawnState previousState;    
    
    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null) {
            Debug.LogError("No spawner referenced");
            this.enabled = false;
        }
        if (waveAnimator == null) {
            Debug.LogError("No waveAnimator referenced");
            this.enabled = false;
        }
        if (waveCountdownText == null) {
            Debug.LogError("No waveCountdownText referenced");
            this.enabled = false;
        }
        if (waveCountText == null) {
            Debug.LogError("No waveCountText referenced");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State) {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
            case WaveSpawner.SpawnState.WAITING:
                break;
            default:
                break;
        }

        previousState = spawner.State;
    }

    void UpdateCountingUI() {
        if (previousState != WaveSpawner.SpawnState.COUNTING) {
            Debug.Log("COUNTING");
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }
    void UpdateSpawningUI() {
        if (previousState != WaveSpawner.SpawnState.SPAWNING) {
            Debug.Log("SPAWNING");
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);
            waveCountText.text = spawner.NextWave.ToString();
        }
    }
}
