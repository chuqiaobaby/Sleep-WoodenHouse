using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public GameObject[] objectsToDeactivate; // objects need to be deactivated
    public Text[] textUI; // texts needed to be display
    public float fadeInTime = 1.0f; // text fade in time
    public float fadeOutTime = 1.0f; // text fade out time
    public float displayTime = 2.0f; // text display time
    public float delayBetweenTexts = 1.0f; // delay between text

    private int currentTextIndex = 0; // current text

    private void Start()
    {
        StartCoroutine(ShowTexts());
    }

    private IEnumerator ShowTexts()
    {
        // deactivate game objects
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        // showing text one by one
        for (int i = 0; i < textUI.Length; i++)
        {
            // hide the texts not showned
            for (int j = i + 1; j < textUI.Length; j++)
            {
                textUI[j].gameObject.SetActive(false);
            }

            // fade in
            StartCoroutine(FadeTextIn(textUI[i]));

            // wait for display time
            yield return new WaitForSeconds(displayTime);

            // fade out
            StartCoroutine(FadeTextOut(textUI[i]));

            // wait for delay time
            yield return new WaitForSeconds(fadeOutTime + delayBetweenTexts);

            // show text after
            for (int j = i + 1; j < textUI.Length; j++)
            {
                textUI[j].gameObject.SetActive(true);
            }
        }

        // re-activate the game objects
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator FadeTextIn(Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); 

        while (text.color.a < 1.0f) 
        {
            float alpha = text.color.a + (Time.deltaTime / fadeInTime); 
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); 
            yield return null;
        }
    }

    private IEnumerator FadeTextOut(Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1); 

        while (text.color.a > 0.0f) 
        {
            float alpha = text.color.a - (Time.deltaTime / fadeOutTime); 
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); 
            yield return null;
        }
    }
}