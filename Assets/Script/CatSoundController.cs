using UnityEngine;
using System.Collections;

public class CatSoundController : MonoBehaviour
{
    public AudioSource audioSource; // Cat Purr Sound
    public float fadeInDuration = 5f; // Fade In Duration in seconds
    public float fadeOutDuration = 50f; // Fade Out Duration in seconds
    private bool isPlaying = false; // Is the cat purr sound playing
    private float startTime; // Starting time
    private float fadeProgress = 0f; // Fade In/Out progress

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f; // Set initial volume to 0
        StartCoroutine(PlayCatPurringDelayed());
    }

    IEnumerator PlayCatPurringDelayed()
    {
        yield return new WaitForSeconds(420f); // Wait for 420 seconds
        isPlaying = true;
        startTime = Time.time;
        Debug.Log("Start Purring");
    }

    void Update()
    {
        if (isPlaying)
        {
            if (fadeProgress < 1f)
            {
                // Fade in the cat purr sound volume
                fadeProgress = (Time.time - startTime) / fadeInDuration;
                audioSource.volume = CustomLerp(0f, 1f, fadeProgress);
            }

            if (fadeProgress >= 1f)
            {
                // Keep cat purr sound volume at max after reaching max volume
                audioSource.volume = 1f;
            }

            // Fade out the cat purr sound volume after playing for 120 seconds
            if (Time.time - startTime >= 170f)
            {
                fadeProgress = (Time.time - startTime - 170) / fadeOutDuration;
                audioSource.volume = CustomLerp(1f, 0f, fadeProgress);
                if (fadeProgress >= 1f)
                {
                    audioSource.Stop();
                    isPlaying = false;
                }
            }
        }

        Debug.Log("Cat Purring Volume: " + (audioSource.volume * 100f).ToString("F2") + "%");
    }

    float CustomLerp(float startValue, float endValue, float progress)
    {
        // Custom linear interpolation function with easing
        progress = Mathf.Clamp01(progress);
        float easedProgress = Mathf.Sin(progress * Mathf.PI * 0.5f);
        return Mathf.Lerp(startValue, endValue, easedProgress);
    }
}
