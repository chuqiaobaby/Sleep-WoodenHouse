using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public ParticleSystem emberParticles;
    public Light fireLight;
    public AudioSource fireAudioSource;
    public float duration = 600f;  // 10mins
    public float fadeDuration = 10f;  // 10 seconds
    private float timer = 0f;

    private float startTime;
    private float initialFireEmissionRate;
    private float initialEmberEmissionRate;
    private float initialIntensity;
    private float initialVolume;
    private float fadeTimer = 0f;
    private bool isFadingIn = true;

    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        initialFireEmissionRate = fireParticles.emission.rateOverTime.constant;
        initialEmberEmissionRate = emberParticles.emission.rateOverTime.constant;
        initialIntensity = fireLight.intensity;
        initialVolume = fireAudioSource.volume;

        fireAudioSource.Play();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float timeElapsed = Time.time - startTime;
        float progress = Mathf.Clamp01(timeElapsed / duration);

        // Print current time to console for Debug
        float currentTime = Time.time;
        Debug.Log("Current Time: " + currentTime);

        // Beginning 10 mins Maximum Fire and Light
        if (progress <= 0.1667f)
        {
            // Maximum intensity and volume
            fireParticles.emissionRate = initialFireEmissionRate;
            emberParticles.emissionRate = initialEmberEmissionRate;
            fireLight.intensity = initialIntensity;
            fireAudioSource.volume = initialVolume;

            // Fade in audio over 10 seconds
            if (isFadingIn)
            {
                fadeTimer += Time.deltaTime;
                float fadeProgress = Mathf.Clamp01(fadeTimer / fadeDuration);
                fireAudioSource.volume = Mathf.Lerp(0f, initialVolume, fadeProgress);

                if (fadeProgress >= 1f)
                {
                    isFadingIn = false;
                    fadeTimer = 0f;
                }
            }

            float percent = 100f;
            Debug.Log("Fire and Light: " + percent.ToString("F0") + "% | Fire Volume: " + (fireAudioSource.volume * 100).ToString("F2") + "%");
        }
        // After first 10 mins, everything decreases to 10% in 20 mins
        else if (progress <= 0.6667f)
        {
            float t = (progress - 0.1667f) / 0.5f;
            float newFireEmissionRate = Mathf.Lerp(initialFireEmissionRate, initialFireEmissionRate * 0.1f, (progress - 0.1667f) / 0.5f);
            float newEmberEmissionRate = Mathf.Lerp(initialEmberEmissionRate, initialEmberEmissionRate * 0.1f, (progress - 0.1667f) / 0.5f);
            float newIntensity = Mathf.Lerp(initialIntensity, initialIntensity * 0.1f, (progress - 0.1667f) / 0.5f);
            float newVolume = Mathf.Lerp(initialVolume, initialVolume * 0.1f, (progress - 0.1667f) / 0.5f);

            fireParticles.emissionRate = newFireEmissionRate;
            emberParticles.emissionRate = newEmberEmissionRate;
            fireLight.intensity = newIntensity;
            fireAudioSource.volume = newVolume;

            float percent = Mathf.Lerp(100f, 10f, t);
            Debug.Log("Fire and Light: " + percent.ToString("F0") + "% | Fire Volume: " + (fireAudioSource.volume * 100).ToString("F2") + "%");

        }
        // Hold the 10% fire, light and volume and stay forever
        else
        {
            fireParticles.emissionRate = initialFireEmissionRate * 0.1f;
            emberParticles.emissionRate = initialEmberEmissionRate * 0.1f;
            fireLight.intensity = initialIntensity * 0.1f;
            fireAudioSource.volume = initialVolume * 0.05f;

            float percent = 10f;
            Debug.Log("Fire and Light: " + percent.ToString("F0") + "% | Fire Volume: " + (fireAudioSource.volume * 100).ToString("F2") + "%");
        }
    }
}




