using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public CinemachineVirtualCamera cinemachine;
    private void Start() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }


    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 3.5f;

    public Transform spawnEffectPrefab;

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
        gm.StartCoroutine(gm.RespawnPlayer());
    }

}
