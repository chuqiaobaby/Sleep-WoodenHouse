using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public Light fireLight;
    public float duration = 5.0f;  // Time for fire to die out

    private float startTime;
    private float initialEmissionRate;
    private float initialIntensity;

    void Start()
    {
        startTime = Time.time;
        initialEmissionRate = fireParticles.emissionRate;
        initialIntensity = fireLight.intensity;
    }

    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float progress = Mathf.Clamp01(timeElapsed / duration);

        // Particle speed decrease
        float newEmissionRate = initialEmissionRate * (1 - progress);
        var emission = fireParticles.emission;
        emission.rateOverTime = newEmissionRate;

        // Point light intensity decrease
        float newIntensity = initialIntensity * (1 - progress);
        fireLight.intensity = newIntensity;
    }
}
