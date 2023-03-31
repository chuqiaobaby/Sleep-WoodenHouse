using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public ParticleSystem emberParticles;
    public Light fireLight;
    public float duration = 5.0f;  // fire die out time

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
    }

    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float progress = Mathf.Clamp01(timeElapsed / duration);

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
    }
}
