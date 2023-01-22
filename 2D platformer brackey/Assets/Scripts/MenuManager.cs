using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    AudioManager audioManager;

    private void Start() {
        audioManager = AudioManager.instance;
        if(audioManager == null) {
            Debug.LogError("No audioManager found");
        }
    }

    public void Quit()
    {
        audioManager.PlaySound("pressButton");
        Application.Quit();
    }


    public void Play()
    {
        audioManager.PlaySound("pressButton");
        SceneManager.LoadScene(1);
    }

    public void OnMouseOver() {
        audioManager.PlaySound("hoverButton");
    }
    
}
