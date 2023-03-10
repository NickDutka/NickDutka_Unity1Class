using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalScore : MonoBehaviour
{
    KnockedOver[] pins;

    public TMP_Text scoreText;

    private int totalScore;

    private void Update()
    {
        foreach (KnockedOver pin in pins)
        {
            if(pin.hasFallen == true)
            {
                OnPinFallen(pin);
            }
        }
    }

    private void OnPinFallen(KnockedOver pin)
    {
        totalScore++;
        scoreText.text = "Total: " + totalScore;
    }
}
