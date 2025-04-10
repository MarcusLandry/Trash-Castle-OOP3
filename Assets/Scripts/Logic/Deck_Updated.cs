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

    public Deck()
    {
        cards = new List<Card>();
        InitializeDeck();
        Shuffle();
    }

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
        cards.Add(new Joker());
        cards.Add(new Joker());
    }

    public void Shuffle()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

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

    public void ResetDeck()
    {
        cards.Clear();
        InitializeDeck();
        Shuffle();
    }

    public int CardsRemaining()
    {
        return cards.Count;
    }

    public void SaveDeckToFile(string filePath)
    {
        DeckWrapper wrapper = new DeckWrapper { cards = cards };
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

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
