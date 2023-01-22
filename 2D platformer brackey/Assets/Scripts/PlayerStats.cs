using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int MaxHealth = 100;
    private int _curHealth;
    public int CurHealth
    {
        get { return _curHealth; }
        set { _curHealth = Mathf.Clamp(value, 0, MaxHealth); }
    }

    public float healthRegenRate = 2f;
    public float runSpeed = 40f;


    void Awake() {
        if (instance == null) {
            instance = this;
        }
       // CurHealth = MaxHealth;
    }
}
