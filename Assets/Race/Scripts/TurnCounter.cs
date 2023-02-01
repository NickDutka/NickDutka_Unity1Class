using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TurnCounter : MonoBehaviour
{
    public TMP_Text turnText;

    private float turnCount = 0;

    public void IncreaseTurn()
    {
        turnCount += 0.5f;
        turnText.text = "Turn: " + turnCount;
    }
}
