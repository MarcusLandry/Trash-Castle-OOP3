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

    // Constructor sets a maximum hand size for gameplay balance.
    public Hand(int maxHandSize = 10)
    {
        _maxHandSize = maxHandSize;
        _cards = new List<Card>();
    }

    // Adds a card to the hand, allowing the player to have more options during their turn.
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

    // Adds multiple cards to the hand for efficiency during gameplay.
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

    // Removes a card at a specific index, allowing for strategic gameplay.
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

    // Removes a specific card from the hand, which is necessary for gameplay actions.
    public bool RemoveCard(Card card)
    {
        return _cards.Remove(card);
    }

    // Retrieves a card from the hand by index, allowing the player to access their cards.
    public Card GetCard(int index)
    {
        if (index < 0 || index >= _cards.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Card index is out of range");
        }

        return _cards[index];
    }

    // Checks if a specific card is in the hand, which is important for gameplay decisions.
    public bool HasCard(Card card)
    {
        return _cards.Contains(card);
    }

    // Checks if the hand contains a card of a specific type, aiding in strategic gameplay.
    public bool HasCardOfType(Card.CardType cardType)
    {
        return _cards.Any(card => card.Type == cardType);
    }

    // Checks if the hand contains a specific number card, which may be needed for game rules.
    public bool HasNumberCard(int value)
    {
        return _cards.Any(card => card.Type == Card.CardType.Number && card.Value == value);
    }

    // Retrieves all number cards from the hand, which may be needed for specific actions.
    public List<Card> GetNumberCards()
    {
        return _cards.Where(card => card.Type == Card.CardType.Number).ToList();
    }

    // Retrieves all special cards from the hand, which may have unique abilities.
    public List<Card> GetSpecialCards()
    {
        return _cards.Where(card => card.IsSpecialCard).ToList();
    }

    // Plays a specific card from the hand, which is necessary for game actions.
    public Card PlayCard(Card card)
    {
        int index = _cards.IndexOf(card);

        if (index == -1)
        {
            return null; // Card not found in hand
        }

        return RemoveCardAt(index);
    }

    // Plays a card of a specific type, allowing for strategic gameplay.
    public Card PlayCardOfType(Card.CardType cardType)
    {
        Card card = _cards.FirstOrDefault(c => c.Type == cardType);

        if (card != null)
        {
            RemoveCard(card);
        }

        return card;
    }

    // Plays a number card of a specific value, which is necessary for game mechanics.
    public Card PlayNumberCard(int value)
    {
        Card card = _cards.FirstOrDefault(c => c.Type == Card.CardType.Number && c.Value == value);

        if (card != null)
        {
            RemoveCard(card);
        }

        return card;
    }

    // Clears the hand at the end of a round, returning the cards for further use.
    public List<Card> ClearHand()
    {
        List<Card> cards = new List<Card>(_cards);
        _cards.Clear();
        return cards;
    }

    // Returns a string representation of the hand, useful for debugging and display.
    public override string ToString()
    {
        if (_cards.Count == 0)
        {
            return "Empty hand";
        }

        return string.Join(", ", _cards.Select(card => card.ToString()));
    }

    // Saves the current state of the hand to a file for persistence.
    public void SaveHandToFile(string filePath)
    {
        HandWrapper wrapper = new HandWrapper { cards = _cards };
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

    // Loads the hand state from a file, allowing players to resume from a saved game.
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
