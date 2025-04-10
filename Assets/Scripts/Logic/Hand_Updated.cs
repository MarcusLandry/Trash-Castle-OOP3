using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HandWrapper
{
    public List<Card> cards;
}

public class Hand
{
    private readonly int _maxHandSize;
    private List<Card> _cards;

    public int CardCount => _cards.Count;
    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    public Hand(int maxHandSize = 10)
    {
        _maxHandSize = maxHandSize;
        _cards = new List<Card>();
    }

    public bool AddCard(Card card)
    {
        if (card == null)
        {
            throw new ArgumentNullException(nameof(card), "Cannot add a null card to hand");
        }

        if (_cards.Count >= _maxHandSize)
        {
            return false; // Hand is full
        }

        _cards.Add(card);
        return true;
    }

    public int AddCards(IEnumerable<Card> cards)
    {
        if (cards == null)
        {
            throw new ArgumentNullException(nameof(cards), "Cannot add null cards to hand");
        }

        int addedCount = 0;

        foreach (var card in cards)
        {
            if (AddCard(card))
            {
                addedCount++;
            }
            else
            {
                break; // Stop if the hand becomes full
            }
        }

        return addedCount;
    }

    public Card RemoveCardAt(int index)
    {
        if (index < 0 || index >= _cards.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Card index is out of range");
        }

        Card card = _cards[index];
        _cards.RemoveAt(index);
        return card;
    }

    public bool RemoveCard(Card card)
    {
        return _cards.Remove(card);
    }

    public Card GetCard(int index)
    {
        if (index < 0 || index >= _cards.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Card index is out of range");
        }

        return _cards[index];
    }

    public bool HasCard(Card card)
    {
        return _cards.Contains(card);
    }

    public bool HasCardOfType(Card.CardType cardType)
    {
        return _cards.Any(card => card.Type == cardType);
    }

    public bool HasNumberCard(int value)
    {
        return _cards.Any(card => card.Type == Card.CardType.Number && card.Value == value);
    }

    public List<Card> GetNumberCards()
    {
        return _cards.Where(card => card.Type == Card.CardType.Number).ToList();
    }

    public List<Card> GetSpecialCards()
    {
        return _cards.Where(card => card.IsSpecialCard).ToList();
    }

    public Card PlayCard(Card card)
    {
        int index = _cards.IndexOf(card);

        if (index == -1)
        {
            return null; // Card not found in hand
        }

        return RemoveCardAt(index);
    }

    public Card PlayCardOfType(Card.CardType cardType)
    {
        Card card = _cards.FirstOrDefault(c => c.Type == cardType);

        if (card != null)
        {
            RemoveCard(card);
        }

        return card;
    }

    public Card PlayNumberCard(int value)
    {
        Card card = _cards.FirstOrDefault(c => c.Type == Card.CardType.Number && c.Value == value);

        if (card != null)
        {
            RemoveCard(card);
        }

        return card;
    }

    public List<Card> ClearHand()
    {
        List<Card> cards = new List<Card>(_cards);
        _cards.Clear();
        return cards;
    }

    public override string ToString()
    {
        if (_cards.Count == 0)
        {
            return "Empty hand";
        }

        return string.Join(", ", _cards.Select(card => card.ToString()));
    }

    public void SaveHandToFile(string filePath)
    {
        HandWrapper wrapper = new HandWrapper { cards = _cards };
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

    public void LoadHandFromFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            HandWrapper wrapper = JsonUtility.FromJson<HandWrapper>(json);
            _cards = wrapper.cards ?? new List<Card>();
        }
    }
}
