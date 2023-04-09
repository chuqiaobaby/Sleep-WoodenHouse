using UnityEngine;
using System.Collections;

public class RainSoundController : MonoBehaviour
{
    public AudioSource audioSource; // Rain Sound
    public float fadeInDuration = 200f; // Fade In Duration in seconds
    private bool isPlaying = false; // Is the rain sound playing
    private float startTime; // Starting time
    private float fadeProgress = 0f; // Fade In progress

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f; // Set initial volume to 0
        StartCoroutine(PlayRainSoundDelayed());
    }

    IEnumerator PlayRainSoundDelayed()
    {
        yield return new WaitForSeconds(530f); // Wait for 55530 seconds
        isPlaying = true;
        startTime = Time.time;
        Debug.Log("Start Rain Sound");
    }

    void Update()
    {
        if (isPlaying)
        {
            if (fadeProgress < 1f)
            {
                // Fade in the rain sound volume
                fadeProgress = (Time.time - startTime) / fadeInDuration;
                audioSource.volume = CustomLerp(0f, 1f, fadeProgress);
            }
            else
            {
                // Keep rain sound volume at max after reaching max volume
                audioSource.volume = 1f;
            }
  
        }

        Debug.Log("Rain Sound Volume: " + (audioSource.volume * 100f).ToString("F2") + "%");
    }

    float CustomLerp(float startValue, float endValue, float progress)
    {
        // Custom linear interpolation function with easing
        progress = Mathf.Clamp01(progress);
        float easedProgress = Mathf.Sin(progress * Mathf.PI * 0.5f);
        return Mathf.Lerp(startValue, endValue, easedProgress);
    }
}

