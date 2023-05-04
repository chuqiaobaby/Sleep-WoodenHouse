using UnityEngine;
using System.Collections;

public class CatOnBedSoundController : MonoBehaviour
{
    public AudioSource catOnBedAudioSource; // Other Sound
    public float fadeInDuration = 5f; // Fade In Duration in seconds
    public float fadeOutDuration = 10f; // Fade Out Duration in seconds
    private bool isPlaying = false; // Is the other sound playing
    private float startTime; // Starting time
    private float fadeProgress = 0f; // Fade In/Out progress

    void Start()
    {
        catOnBedAudioSource = GetComponent<AudioSource>();
        catOnBedAudioSource.volume = 0f; // Set initial volume to 0
        StartCoroutine(PlayOtherSoundDelayed());
    }

    IEnumerator PlayOtherSoundDelayed()
    {
        yield return new WaitForSeconds(300f); // Wait for 400 seconds
        isPlaying = true;
        startTime = Time.time;
        Debug.Log("Start Other Sound");
    }

    void Update()
    {
        if (isPlaying)
        {
            if (fadeProgress < 1f)
            {
                // Fade in the other sound volume
                fadeProgress = (Time.time - startTime) / fadeInDuration;
                catOnBedAudioSource.volume = CustomLerp(0f, 1f, fadeProgress);
            }

            if (fadeProgress >= 1f)
            {
                // Keep other sound volume at max after reaching max volume
                catOnBedAudioSource.volume = 1f;
            }

            // Fade out the other sound volume after playing for 120 seconds
            if (Time.time - startTime >= 15f)
            {
                fadeProgress = (Time.time - startTime - 15f) / fadeOutDuration;
                catOnBedAudioSource.volume = CustomLerp(1f, 0f, fadeProgress);
                if (fadeProgress >= 1f)
                {
                    catOnBedAudioSource.Stop();
                    isPlaying = false;
                }
            }
        }

        Debug.Log("Cat On Bed Sound Volume: " + (catOnBedAudioSource.volume * 100f).ToString("F2") + "%");
    }

    float CustomLerp(float startValue, float endValue, float progress)
    {
        // Custom linear interpolation function with easing
        progress = Mathf.Clamp01(progress);
        float easedProgress = Mathf.Sin(progress * Mathf.PI * 0.5f);
        return Mathf.Lerp(startValue, endValue, easedProgress);
    }
}
