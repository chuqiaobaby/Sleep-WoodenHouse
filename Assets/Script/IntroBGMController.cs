using UnityEngine;
using System.Collections;

public class IntroBGMController : MonoBehaviour
{
    public AudioSource audioSource; // IntroBGM audio
    public float fadeInDuration = 2f; // fade in
    public float fadeOutDuration = 2f; // fade out
    public float playDuration = 15f; // duration before fade out
    private bool isPlaying = false;
    private float startTime;
    private float fadeProgress = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        PlayIntroBGM();
    }

    void PlayIntroBGM()
    {
        isPlaying = true;
        startTime = Time.time;
        audioSource.Play();
        Debug.Log("IntroBGM开始播放");
    }

    void Update()
    {
        if (isPlaying)
        {
            if (fadeProgress < 1f && Time.time - startTime < fadeInDuration)
            {
                // fade in
                fadeProgress = (Time.time - startTime) / fadeInDuration;
                audioSource.volume = CustomLerp(0f, 1f, fadeProgress);
            }
            else if (Time.time - startTime >= playDuration && fadeProgress < 1f && Time.time - startTime < fadeInDuration + fadeOutDuration + playDuration)
            {
                // fade out
                fadeProgress = (Time.time - startTime - playDuration) / fadeOutDuration;
                audioSource.volume = CustomLerp(1f, 0f, fadeProgress);
            }
            else if (fadeProgress >= 1f)
            {
                // stay at 0% volume
                audioSource.volume = 0f;
                audioSource.Stop();
                isPlaying = false;
            }
        }

        Debug.Log("IntroBGM Volume: " + (audioSource.volume * 100f).ToString("F2") + "%");
    }

    float CustomLerp(float startValue, float endValue, float progress)
    {
        progress = Mathf.Clamp01(progress);
        float easedProgress = Mathf.Sin(progress * Mathf.PI * 0.5f);
        return Mathf.Lerp(startValue, endValue, easedProgress);
    }
}



