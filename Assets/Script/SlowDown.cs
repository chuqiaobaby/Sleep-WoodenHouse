using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public float startDelay = 180f; // wait time
    public float slowDownDuration = 120f; // duration of slowing down
    public float slowDownAmount = 0.3f; // aim simulation speed
    private float slowDownTimer = 0f; // timer
    public ParticleSystem[] particleSystems;

    private void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        StartCoroutine(SlowDownTime());
    }

    private IEnumerator SlowDownTime()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            float timeElapsed = 0f;
            float initialSpeed = particleSystems[0].main.simulationSpeed;
            while (timeElapsed < slowDownDuration)
            {
                timeElapsed += Time.deltaTime;
                float slowAmount = Mathf.Lerp(initialSpeed, slowDownAmount, timeElapsed / slowDownDuration);
                foreach (var ps in particleSystems)
                {
                    var main = ps.main;
                    main.simulationSpeed = slowAmount;
                }
                yield return null;
            }
            foreach (var ps in particleSystems)
            {
                var main = ps.main;
                main.simulationSpeed = slowDownAmount;
            }
            yield return new WaitForSeconds(0.5f); 
        }
    }
}