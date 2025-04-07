using System;
using System.Collections.Generic;

public class Deck
{
    private List<Card> cards;
    private Random rng = new Random();

    /// <summary>
    /// Initializes the deck with all possible cards and shuffles it.
    /// </summary>
    public Deck()
    {
        cards = new List<Card>();
        InitializeDeck();
        Shuffle();
    }

    /// <summary>
    /// Populates the deck with standard playing cards.
    /// </summary>
    private void InitializeDeck()
    {
        // Add Number Cards (2-10) for each suit
        foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
        {
            for (int value = 2; value <= 10; value++)
            {
                cards.Add(new NumberCard(suit, value));
            }

            // Add Face Cards (Jack, Queen, King, Ace)
            cards.Add(new Jack(suit));
            cards.Add(new Queen(suit));
            cards.Add(new King(suit));
            cards.Add(new Ace(suit));
        }

        // Add Jokers (assuming 2 in a deck)
        cards.Add(new Joker());
        cards.Add(new Joker());
    }

    /// <summary>
    /// Shuffles the deck randomly.
    /// </summary>
    public void Shuffle()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    /// <summary>
    /// Draws a card from the top of the deck.
    /// </summary>
    /// <returns>The drawn card, or null if the deck is empty.</returns>
    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            return null; // Deck is empty
        }

        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    /// <summary>
    /// Resets the deck by re-initializing and shuffling it.
    /// </summary>
    public void ResetDeck()
    {
        cards.Clear();
        InitializeDeck();
        Shuffle();
    }

    /// <summary>
    /// Returns the number of cards left in the deck.
    /// </summary>
    public int CardsRemaining()
    {
        return cards.Count;
    }
}

