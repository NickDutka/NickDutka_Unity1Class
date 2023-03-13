using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    public TMP_Text text;
    public float fadeTime = 1f;

    private void Start()
    {
        // Start the coroutine when the object is enabled
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Get the initial alpha value of the text
        Color startColor = text.color;
        float alpha = startColor.a;

        // Gradually reduce the alpha value over time
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            alpha = Mathf.Lerp(startColor.a, 0f, t / fadeTime);
            text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // Ensure the text is completely transparent
        text.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}
