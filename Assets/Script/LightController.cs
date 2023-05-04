using UnityEngine;

public class LightController : MonoBehaviour
{
    public float delayInSeconds = 5f; // delay time
    public AudioSource switchAudioSource; // Switch audio
    public Light[] lightsToSwitch; // list of point light

    private float timer; 
    private bool isSwitched; 

    void Update()
    {
      
        if (timer < delayInSeconds && !isSwitched)
        {
            timer += Time.deltaTime; 
        }
        else if (timer >= delayInSeconds && !isSwitched)
        {
            isSwitched = true; 

            
            foreach (Light light in lightsToSwitch)
            {
                
                switchAudioSource.PlayOneShot(switchAudioSource.clip);
                light.enabled = false;
            }
        }
    }
}



