using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake_coroutine : MonoBehaviour
{
    //public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    private float ShakeAmplitude = 0f;         // Cinemachine Noise Profile Parameter
    private float ShakeFrequency = 0f;         // Cinemachine Noise Profile Parameter
    private bool isShaking = true;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start() {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update() {
        /*
        if (Input.GetKey(KeyCode.T)) {
            //ShakeElapsedTime = ShakeDuration;
            Shake(ShakeDuration, ShakeAmplitude, ShakeFrequency);
        }
        */        
    }
    public void Shake(float duration, float amplitude, float frequency) {
        ShakeAmplitude = amplitude;
        ShakeFrequency = frequency;
            
        if (isShaking) {
            CancelInvoke();
            StopShake();
        }
        BeginShake();
        Invoke("StopShake", duration);
    }

    void BeginShake() {
        isShaking = true;
        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null) {
            // Set Cinemachine Camera Noise parameters
            virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
            virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
        }
    }
    void StopShake() {
        isShaking = false;
        virtualCameraNoise.m_AmplitudeGain = 0f;
        virtualCameraNoise.m_FrequencyGain = 0f;
    }
}