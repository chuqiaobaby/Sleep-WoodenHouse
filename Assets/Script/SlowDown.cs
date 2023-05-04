using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public float startDelay = 180f; // 游戏开始后的等待时间（秒）
    public float slowDownDuration = 120f; // 减速持续时间（秒）
    public float slowDownAmount = 0.3f; // 减速量
    private float slowDownTimer = 0f; // 减速计时器
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
            yield return new WaitForSeconds(0.5f); // 等待0.5秒再循环，以避免协程死循环
        }
    }
}