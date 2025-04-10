using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using UnityEngine;
=======
using static Card;
>>>>>>> card-and-hand-classes

[Serializable]
public class HandWrapper
{
    public List<Card> cards;
}

<<<<<<< HEAD
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
=======
/// <summary>
/// Represents a player's hand of cards in the Trash Castle game.
/// Manages the cards a player currently holds and provides methods to add, remove, and play cards.
/// </summary>
    public class Hand
    {
        public string PlayerName { get; private set; }

        // defines if a hand belongs to the user or AI
        public bool IsAI { get; private set; }
        public int Castle { get; set; }
        public List<Card> Cards { get; private set; }
        // 2-10 grid of cards
        public List<Card> CollectionGrid { get; private set; } = new List<Card>();

    /// <summary>
    /// Initializes a new instance of the Hand class 
    /// </summary>
    /// <param name="maxHandSize">The maximum number of cards this hand can hold</param>
        public Hand(string playerName, bool isAI = false)
        {
            PlayerName = playerName;
            IsAI = isAI;
            Castle = 50; // Default health of the castle
            Cards = new List<Card>();
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool HasNumberInCollection(int value)
    {
        return CollectionGrid.Any(c => c.Value == value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="card"></param>
    public void AddToCollection(Card card)
    {
        if (card.Type == CardType.Number && card.Value.HasValue)
        {
            if (!HasNumberInCollection(card.Value.Value))
            {
                CollectionGrid.Add(card);
                Debug.Log($"{PlayerName} added {card.Name} to their collection grid.");
            }
            else
            {
                Debug.Log($"{PlayerName} already has number {card.Value} in their collection. Card discarded.");
                // You can discard or handle it however you'd like here
            }
        }
    }

    /// <summary>
    /// Adds a card to the hand if there is room
    /// </summary>
    /// <param name="card">The card to add</param>
    /// <returns>True if the card was added successfully, false if the hand is full</returns>
    public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        /// <summary>
        /// Removes a card from the hand by index
        /// </summary>
        /// <param name="index">The index of the card to remove</param>
        /// <returns>The removed card</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range</exception>
        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }


        public int TakeDamage(Card card) 
        {
            this.Castle = Castle - card.Damage;
            return this.Castle;
        }

        
        /// <summary>
        /// Gets a card at a specific index without removing it
        /// </summary>
        /// <param name="index">The index of the card to get</param>
        /// <returns>The card at the specified index</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range</exception>
        public Card GetCard(int index)
        {
            if (index < 0 || index >= Cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Card index is out of range");
            }
            
            return Cards[index];
        }

        /// <summary>
        /// Checks if the hand contains a specific card
        /// </summary>
        /// <param name="card">The card to check for</param>
        /// <returns>True if the hand contains the card, false otherwise</returns>
        public bool HasCard(Card card)
        { 
            return Cards.Contains(card);
>>>>>>> card-and-hand-classes
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
<<<<<<< HEAD
            string json = System.IO.File.ReadAllText(filePath);
            HandWrapper wrapper = JsonUtility.FromJson<HandWrapper>(json);
            _cards = wrapper.cards ?? new List<Card>();
        }
=======
            return Cards.Any(card => card.Type == Card.CardType.Number && card.Value == value);
        }
        
        /// <summary>
        /// Gets all the number cards in the hand
        /// </summary>
        /// <returns>A list of number cards</returns>
        public List<Card> GetNumberCards()
        {
            return Cards.Where(card => card.Type == Card.CardType.Number).ToList();
        }
        
        /// <summary>
        /// Gets all the special cards in the hand
        /// </summary>
        /// <returns>A list of special cards</returns>
        public List<Card> GetSpecialCards()
        {
            return Cards.Where(card => card.IsSpecialCard).ToList();
        }
        
        /// <summary>
        /// Clears all cards from the hand
        /// </summary>
        /// <returns>The list of cards that were in the hand</returns>
        public List<Card> ClearHand()
        {
            List<Card> cards = new List<Card>(Cards);
            Cards.Clear();
            return cards;
        }
        
>>>>>>> card-and-hand-classes
    }
}
