using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BlackJackLogic : MonoBehaviour
{ 
    Card cardPrefab;
    public GameObject cardGO;

    List<Card> deck = new List<Card>();

    List<Card> playerCards = new List<Card>();

    List<Card> dealerCards = new List<Card>();

    public Vector3 cardpos1;
    public Vector3 cardpos2;
    public Vector3 cardpos3;
    public Vector3 cardpos4;

    public int playerCardXOffset;
    public int dealerCardXOffset;

    public TMP_Text playerScoreText;
    public TMP_Text dealerScoreText;
    public TMP_Text playerGOScoreText;
    public TMP_Text dealerGOScoreText;
    public TMP_Text bustText;

    public int playerScore;
    public int dealerScore;

    public GameObject gameCanvas;
    public GameObject gameoverCanvas;

    public GameObject dealInitialButton;
    public GameObject hitbutton;
    public GameObject staybutton;


    private void Start()
    {
        // Initializes and builds deck of cards
        gameoverCanvas.SetActive(false);
        dealInitialButton.SetActive(false);
        hitbutton.SetActive(false);
        staybutton.SetActive(false);

        cardPrefab = cardGO.GetComponent<Card>();

        List<string> names = new List<string>()
        {
            "Ace",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten",
            "Jack",
            "Queen",
            "King"
        };

        for (int s = 0; s < 4; s++)
        {
            for (int v = 0; v < 13; v++)
            {
                Card.Suit suit = (Card.Suit)s;

                int score;

                if (v < 10)
                {
                    score = v + 1;
                }
                else
                {
                    score = 10;
                }

                Card card = Instantiate(cardPrefab);

                card.Initialize(names[v], score, suit);

                deck.Add(card);
            }
        }

        foreach (Card card in deck)
        {
            Debug.Log(card.ToString());
        }
    }

    public void ShuffleDeck()
    {
        // suffle the list index for the deck

        for (int i = 0; i < deck.Count; i++)
        {
            Card shuffledindex = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = shuffledindex;
        }

        foreach (Card card in deck)
        {
            Debug.Log(card.ToString());
        }
    }

    public void DealInitialCards()
    {
            // Deal 2 cards to player

        Card deltCard1 = deck[deck.Count - 1];

        deltCard1.transform.position = cardpos1;

        deck.Remove(deck[deck.Count - 1]);

        playerCards.Add(deltCard1);

        Debug.Log(deltCard1);

        Card deltCard2 = deck[deck.Count - 1];

        deltCard2.transform.position = cardpos2;

        deck.Remove(deck[deck.Count - 1]);

        playerCards.Add(deltCard2);

        Debug.Log(deltCard2);

            // Deal 2 cards to dealer

        Card deltCard3 = deck[deck.Count - 1];

        deltCard3.transform.position = cardpos3;
        deltCard3.transform.rotation = new Quaternion(0, 0, 180, 0);

        deck.Remove(deck[deck.Count - 1]);

        dealerCards.Add(deltCard3);

        Debug.Log(deltCard3);

        Card deltCard4 = deck[deck.Count - 1];

        deltCard4.transform.position = cardpos4;

        deck.Remove(deck[deck.Count - 1]);

        dealerCards.Add(deltCard4);

        Debug.Log(deltCard4);

        playerCardXOffset = 4;

        dealerCardXOffset = 4;

        UpdateScore();
    }

    public void UpdateScore()
    {
        // Updates score and score text values

        playerScore = 0;
        dealerScore = 0;
        
        foreach(Card card in playerCards)
        {
            playerScore += card.value;

        }
        
        playerScoreText.text = "Player Score: " + playerScore.ToString();
        playerGOScoreText.text = "Player Score: " + playerScore.ToString();

        Debug.Log(playerScore);

        foreach (Card card in dealerCards)
        {
            dealerScore += card.value;
        }

        dealerScoreText.text = "Dealer Score: " + dealerScore.ToString();
        dealerGOScoreText.text = "Dealer Score: " + dealerScore.ToString();

        Debug.Log(dealerScore);

        Bust();
        BlackJack();
    }

    public void Stay()
    {
        // Disable Hit Button
        // Ends players turn

        // Makes dealer either draw if score is under 17 or stay if score is 17 or more

        while (dealerScore < 17)
        {
            Debug.Log("dealer chooses to hit");

            DealerHits();
            if (dealerScore >= 17)
            {
                DealerStays();
                Debug.Log("DealerStays");
            }
        }
       
    }

    public void DealerHits()
    {
        dealerCardXOffset += 2;

        Card dealerDeltCard = deck[deck.Count - 1];

        deck.Remove(deck[deck.Count - 1]);

        dealerCards.Add(dealerDeltCard);

        dealerDeltCard.transform.position = new Vector3(dealerCardXOffset, 0.1f, -2);

        Debug.Log(dealerDeltCard);

        UpdateScore();

    }

    public void Bust()
    {
        if (playerScore >= 22)
        {
            gameCanvas.SetActive(false);
            gameoverCanvas.SetActive(true);

            bustText.text = "Player is Bust! - Dealer Wins!";
        }
        if (dealerScore >= 22)
        {
            gameCanvas.SetActive(false);
            gameoverCanvas.SetActive(true);

            bustText.text = "Dealer is Bust! - Player Wins!";
        }

    }

    public void BlackJack()
    {
        if (playerScore == 21)
        {
            gameCanvas.SetActive(false);
            gameoverCanvas.SetActive(true);

            bustText.text = "BlackJack! Player wins!";
        }
        if (dealerScore == 21)
        {
            gameCanvas.SetActive(false);
            gameoverCanvas.SetActive(true);

            bustText.text = "Blackjack! Dealer wins!";
        }

    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DealerStays()
    {
        
        
            if (dealerScore < 22)
            {
                UpdateScore();
                if (playerScore > dealerScore)

                bustText.text = "Player wins!";
            }
            else if (dealerScore > 22)
            {
                UpdateScore();
            
            }
            
            
        
    }

    // deals player a card if they press button, or gets
    // called during dealer turn to deal themselves a card.
    public void DealPlayerCard()
    {
        playerCardXOffset += 2;

        Card deltCard = deck[deck.Count - 1];

        deck.Remove(deck[deck.Count - 1]);

        playerCards.Add(deltCard);

        deltCard.transform.position = new Vector3(playerCardXOffset,0.1f,2);
        
        Debug.Log(deltCard);

        UpdateScore();
    }

    //private void Update()
    //{
    //    Bust();
    //}

}


