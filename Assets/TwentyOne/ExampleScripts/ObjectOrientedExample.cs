using UnityEngine;
using TMPro;

public class ObjectOrientedExample : MonoBehaviour
{                 
    public GameObject cardPrefab;
    public TextMeshProUGUI scoreText;

    public float spacing = 2;

    private int score;

    // We are playing a game with a deck of cards
    // where each card can be either "Red" or "Black"
    // The dealer will deal 5 random cards to the player.
    // Red cards are worth 1, black cards are worth 2.
    private void Start()
    {
        score = 0;

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = Vector3.zero;
            position.x = i * spacing;

            GameObject card = Instantiate(cardPrefab);

            card.transform.position = position;

            if (Random.value > 0.5f)
            {
                // Red
                //card.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                //card.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;

                // "Ecapsulation", to hide something within something else.
                // "Abstraction", to view something as simpler than its internal parts.
                card.GetComponent<CardExample>().SetColor(CardExample.CardColor.Red);

                score += 1;
            }
            else
            {
                // Black
                card.GetComponent<CardExample>().SetColor(CardExample.CardColor.Black);

                score += 2;
            }
        }

        scoreText.text = $"Your score is: {score}";
    }

    // Deal a new card, and recalculate the score.
    // How do we position the new card? How do we know
    // how many cards have been dealt?
    // (Not solved in class).
    public void Deal()
    {
        GameObject card = Instantiate(cardPrefab);

        if (Random.value > 0.5f)
        {
            card.GetComponent<CardExample>().SetColor(CardExample.CardColor.Red);

            score += 1;
        }
        else
        {
            card.GetComponent<CardExample>().SetColor(CardExample.CardColor.Black);

            score += 2;
        }

        scoreText.text = $"Your score is: {score}";
    }
}
