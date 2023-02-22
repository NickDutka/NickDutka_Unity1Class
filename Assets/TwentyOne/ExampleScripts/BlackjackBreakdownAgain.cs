using UnityEngine;
using System.Collections.Generic;

public class BlackjackBreakdownAgain : MonoBehaviour
{
    public Card cardPrefab;

    // Card in a standard western 52-card deck
    // string suit = "Hearts";

    // 0, 1, 2, 3
    // 0 = hearts, 1 = spades...
    // int suit = 2;

    // Erick also mentioned--are suits actually part of blackjack?
    enum Suit { Hearts, Spades }

    // This is a card;
    //string name = "Jack";
    //int value = 10;
    //Suit suit = Suit.Hearts;

    //string[] names = new string[] { "Ace", "Ace", "Ace" };
    //int[] values = new int[] { 1, 1, 1 };
    //Suit[] suits = new Suit[] { Suit.Hearts, Suit.Spades, Suit.Hearts };

    private class MyCard : object
    {
        public string name;

        /// <summary>
        /// The score this card is worth in blackjack.
        /// </summary>
        public int value;

        public Suit suit;

        public MyCard(string name, int value, Suit suit)
        {
            this.name = name;
            this.value = value;
            this.suit = suit;
        }

        public override string ToString()
        {
            return $"{name} of {suit} worth {value}";
        }
    }

    List<MyCard> deck = new List<MyCard>();

    private void Start()
    {
        //Card mySpawnedCard = Instantiate(cardPrefab);
        //mySpawnedCard.Initialize("Four", 4, Card.Suit.Hearts);

        //MyCard fourOfHearts = new MyCard("Four", 4, Suit.Hearts);
        //MyCard aceOfSpades = new MyCard("Ace", 1, Suit.Spades);

        //deck.Add(fourOfHearts);
        //deck.Add(aceOfSpades);
        //deck.Add(new MyCard("Five", 5, Suit.Hearts));
        //deck.Add(new MyCard("Six", 6, Suit.Hearts));

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

        for (int s = 0; s < 2; s++)
        {
            for (int v = 0; v < 13; v++)
            {
                Suit suit = (Suit)s;

                int score;

                if (v < 10)
                {
                    score = v + 1;
                }
                else
                {
                    score = 10;
                }

                MyCard card = new MyCard(names[v], score, suit);

                deck.Add(card);
            }
        }

        //foreach (MyCard card in deck)
        //{
        //    Debug.Log(card.ToString());
        //}

        int selectedCardIndex = Random.Range(0, deck.Count);

        MyCard deltCard = deck[selectedCardIndex];
        deck.RemoveAt(selectedCardIndex);

        Debug.Log(deltCard);
    }
}
