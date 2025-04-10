using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeckWrapper
{
    public List<Card> cards;
}

public class Deck
{
    private List<Card> cards;
    private System.Random rng = new System.Random();

    // Constructor initializes the deck and shuffles it for randomness.
    public Deck()
    {
        cards = new List<Card>();
        InitializeDeck();
        Shuffle();
    }

    // Populates the deck with a full set of cards, ensuring all types are available for gameplay.
    private void InitializeDeck()
    {
        foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
        {
            for (int value = 2; value <= 10; value++)
            {
                cards.Add(new NumberCard(suit, value));
            }
            cards.Add(new Jack(suit));
            cards.Add(new Queen(suit));
            cards.Add(new King(suit));
            cards.Add(new Ace(suit));
        }

        // Add Joker (only one because Joker is OP)
        cards.Add(new Joker());
    }

    // Shuffles the deck to ensure fair gameplay by randomizing card order.
    public void Shuffle()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    // Draws a card from the deck for the player to use during their turn.
    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    // Resets the deck for a new game by clearing and reinitializing it.
    public void ResetDeck()
    {
        cards.Clear();
        InitializeDeck();
        Shuffle();
    }

    // Returns the number of cards remaining in the deck, useful for game logic.
    public int CardsRemaining()
    {
        return cards.Count;
    }

    // Saves the current state of the deck to a file for persistence.
    public void SaveDeckToFile(string filePath)
    {
        DeckWrapper wrapper = new DeckWrapper { cards = cards };
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

    // Loads the deck state from a file, allowing players to resume from a saved game.
    public void LoadDeckFromFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            DeckWrapper wrapper = JsonUtility.FromJson<DeckWrapper>(json);
            cards = wrapper.cards ?? new List<Card>();
        }
    }
}
