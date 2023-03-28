using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public TMP_Text introText;
    public float fadeTime = 1f;
    public float pauseTime = 10f;
    public float intropauseTime = 3f;

    public GameObject mainCanvas;

    private void Start()
    {

        mainCanvas.SetActive(false);
        // Start the coroutine when the object is enabled
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        
        // Get the initial alpha value of the text
        Color startColor = introText.color;
        float alpha = 0f; // Change from 1f to 0f

        // Set the text to be completely transparent initially
        introText.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        yield return new WaitForSeconds(intropauseTime);


        // Gradually increase the alpha value to fully visible
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            alpha = Mathf.Lerp(0f, startColor.a, t / fadeTime); // Change from startColor.a to 1f
            introText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // Ensure the text is completely visible
        introText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // Pause for the specified time
        yield return new WaitForSeconds(pauseTime);

        // Gradually reduce the alpha value over time
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            alpha = Mathf.Lerp(startColor.a, 0f, t / fadeTime);
            introText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // Ensure the text is completely transparent
        introText.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        mainCanvas.SetActive(true);
        // Disable the object when done
        gameObject.SetActive(false);
    }
}