using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LivesCounter : MonoBehaviour
{
    [SerializeField]
    TMP_Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        if (livesText == null) {
            Debug.LogError("No livesText referenced");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "LIVES: "+GameMaster.RemainingLives.ToString();
    }
}
