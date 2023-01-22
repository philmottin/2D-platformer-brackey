using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class GameOverUI : MonoBehaviour
{
    //cache
    private AudioManager audioManager;

    public void Quit() {
        audioManager.PlaySound("pressButton");

        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Retry() {
        audioManager.PlaySound("pressButton");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
        // Start is called before the first frame update
    void Start()
    {
        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.Log("No AudioManager found in the scene");
        }

    }

    public void OnMouseOver() {
        audioManager.PlaySound("hoverButton");

    }    
}
