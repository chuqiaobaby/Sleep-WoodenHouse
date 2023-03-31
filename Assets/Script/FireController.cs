using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public ParticleSystem emberParticles;
    public Light fireLight;
    public AudioSource fireAudioSource;
    public float duration = 5.0f;  // fire die out time
    private float timer = 0f;

    private float startTime;
    private float initialFireEmissionRate;
    private float initialEmberEmissionRate;
    private float initialIntensity;

    void Start()
    {
        startTime = Time.time;
        initialFireEmissionRate = fireParticles.emissionRate;
        initialEmberEmissionRate = emberParticles.emissionRate;
        initialIntensity = fireLight.intensity;

        fireAudioSource.Play();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float timeElapsed = Time.time - startTime;
        float progress = Mathf.Clamp01(timeElapsed / duration);
        float volume = Mathf.Lerp(1f, 0f, timer / duration);

        // reduce fire particle speed
        float newFireEmissionRate = initialFireEmissionRate * (1 - progress);
        var fireEmission = fireParticles.emission;
        fireEmission.rateOverTime = newFireEmissionRate;

        // reduce ember particle speed
        float newEmberEmissionRate = initialEmberEmissionRate * (1 - progress);
        var emberEmission = emberParticles.emission;
        emberEmission.rateOverTime = newEmberEmissionRate;

        // reduce light intensity
        float newIntensity = initialIntensity * (1 - progress);
        fireLight.intensity = newIntensity;

        // calculate audio volume
        fireAudioSource.volume = volume;

        // destory object when time run out
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}