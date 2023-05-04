using UnityEngine;
using System.Collections;

public class ParticleSpeedController : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float slowDownTime = 180f;

    public ParticleSystem frameParticleSystem;
    public ParticleSystem emberParticleSystem;
    private float currentSpeed;

    void Start()
    {
        frameParticleSystem = transform.Find("Frame").GetComponent<ParticleSystem>();
        emberParticleSystem = transform.Find("Ember").GetComponent<ParticleSystem>();
        currentSpeed = initialSpeed;

        frameParticleSystem.playbackSpeed = currentSpeed;
        emberParticleSystem.playbackSpeed = currentSpeed;
    }

    void Update()
    {
        if (Time.time > 150f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, initialSpeed * 0.1f, (Time.time - 150f) / slowDownTime);
            frameParticleSystem.playbackSpeed = currentSpeed;
            emberParticleSystem.playbackSpeed = currentSpeed;
            Debug.Log("Frame Particle Speed: " + frameParticleSystem.playbackSpeed + " | Ember Particle Speed: " + emberParticleSystem.playbackSpeed);
        }

        Debug.Log("Frame Particle System: " + (frameParticleSystem == null ? "null" : "not null"));

    }
}

