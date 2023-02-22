using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public string cardName;
    public int value;
    public Suit suit;

    public TextMeshPro cardText;

    public enum Suit { Hearts, Spades, Clubs, Diamonds }
    
    public void Initialize(string cardName, int value, Suit suit)
    {
        this.cardName = cardName;
        this.value = value;
        this.suit = suit;

        cardText.text = $"{cardName} of {suit}";
    }

    public override string ToString()
    {
        return $"{cardName} of {suit} worth {value}";
    }
}
