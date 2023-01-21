using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private TMP_Text healthText;


    // Start is called before the first frame update
    void Start() {
        if (healthBarRect == null) {
            Debug.LogError("No health bar object reference found");
        }
        if (healthText == null) {
            Debug.LogError("No health text object reference found");
        }
    }

    public void SetHealth(int _cur, int _max) {
        float _value = (float)_cur / _max;
        
        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = _cur + " / " + _max + " HP";
        if (_value <= 0.25f) {
            healthBarRect.GetComponent<Image>().color = Color.red;
        } else if (_value <= 0.5f) {
            healthBarRect.GetComponent<Image>().color = Color.yellow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
