using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public GameObject[] objectsToDeactivate; // 需要在显示文字时被deactivate的游戏对象数组
    public Text[] textUI; // 用于显示文字的Text组件数组
    public float fadeInTime = 1.0f; // 文字淡入时间
    public float fadeOutTime = 1.0f; // 文字淡出时间
    public float displayTime = 2.0f; // 文字展示时间
    public float delayBetweenTexts = 1.0f; // 文字之间的延迟时间

    private int currentTextIndex = 0; // 当前要显示的文字索引

    private void Start()
    {
        StartCoroutine(ShowTexts());
    }

    private IEnumerator ShowTexts()
    {
        // 先将要deactivate的对象设置为非活跃状态
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        // 循环显示所有的文字
        for (int i = 0; i < textUI.Length; i++)
        {
            // 隐藏后面的文字
            for (int j = i + 1; j < textUI.Length; j++)
            {
                textUI[j].gameObject.SetActive(false);
            }

            // 淡入文字
            StartCoroutine(FadeTextIn(textUI[i]));

            // 等待文字展示时间
            yield return new WaitForSeconds(displayTime);

            // 淡出文字
            StartCoroutine(FadeTextOut(textUI[i]));

            // 等待文字淡出时间和文字之间的延迟时间
            yield return new WaitForSeconds(fadeOutTime + delayBetweenTexts);

            // 恢复后面的文字的状态
            for (int j = i + 1; j < textUI.Length; j++)
            {
                textUI[j].gameObject.SetActive(true);
            }
        }

        // 将要deactivate的对象重新设置为活跃状态
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator FadeTextIn(Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // 将文字的alpha值设置为0，即完全透明

        while (text.color.a < 1.0f) // 循环，直到文字完全不透明
        {
            float alpha = text.color.a + (Time.deltaTime / fadeInTime); // 根据时间渐变alpha值
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); // 更新文字的alpha值
            yield return null;
        }
    }

    private IEnumerator FadeTextOut(Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1); // 将文字的alpha值设置为1，即完全不透明

        while (text.color.a > 0.0f) // 循环，直到文字完全透明
        {
            float alpha = text.color.a - (Time.deltaTime / fadeOutTime); // 根据时间渐变alpha值
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); // 更新文字的alpha值
            yield return null;
        }
    }
}