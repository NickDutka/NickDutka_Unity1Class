using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public KnockedOver[] pins;

    public TMP_Text scoreText;
    public TMP_Text messageText;
    public TMP_Text roundOneScore, roundTwoScore, totalScore;
    public GameObject strikeText;
    public GameObject messageTextObject;

    public BowlingGameManager bowlingGameManager;

    public int turnOneScore = 0, turnTwoScore = 0;

    public int finalScore;


    private void Start()
    {
        FindPins();
    }
    private void Update()
    {


        if (bowlingGameManager.currentState == BowlingGameManager.State.PreLaunch && bowlingGameManager.turnCounter == 1)
        {
            scoreText.text = "Score: " + turnOneScore;

        }

        if (bowlingGameManager.currentState == BowlingGameManager.State.PostLaunch && bowlingGameManager.turnCounter == 1)
        {

            foreach (KnockedOver pin in pins)
            {
                if (pin.score == 1 && pin.counted == false)
                {
                    turnOneScore++;
                    pin.counted = true;
                    Debug.Log("Score " + turnOneScore);
                }

            }

            scoreText.text = "Score: " + turnOneScore;

            roundOneScore.text = "Round 1: " + turnOneScore;
        }

        if (bowlingGameManager.currentState == BowlingGameManager.State.PreLaunch && bowlingGameManager.turnCounter == 2)
        {
            scoreText.text = "Score: " + turnTwoScore;

        }

        if (bowlingGameManager.currentState == BowlingGameManager.State.PostLaunch && bowlingGameManager.turnCounter == 2)
        {

            foreach (KnockedOver pin in pins)
            {
                if (pin.score == 1 && pin.counted == false)
                {
                    turnTwoScore++;
                    pin.counted = true;
                    Debug.Log("Score " + turnOneScore);
                }

            }

            scoreText.text = "Score: " + turnTwoScore;

            roundTwoScore.text = "Round 2: " + turnTwoScore;

            CalculateFinalScore();

        }

        if (turnOneScore == 10 || turnTwoScore == 10)
        {
            strikeText.SetActive(true);
            messageTextObject.SetActive(true);
        }
    }

    public void FindPins()
    {
        pins = FindObjectsOfType<KnockedOver>();
    }
    public void CalculateFinalScore()
    {
        finalScore = turnOneScore + turnTwoScore;

        totalScore.text = "Total Score: " + finalScore;
    }
}
