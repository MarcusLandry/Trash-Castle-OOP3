using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Card;

[Serializable]
public class HandWrapper
{
    public List<Card> cards;
}

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
        public int CardCount => Cards.Count;
        public IReadOnlyList<Card> CardsRO => Cards.AsReadOnly();

    /// <summary>
    /// Initializes a new instance of the Hand class 
    /// </summary>
    /// <param name="maxHandSize">The maximum number of cards this hand can hold</param>
        public Hand(string playerName, bool isAI = false, int maxHandSize = 10)
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

        public Card RemoveCardAt(int index)
        {
        if (index < 0 || index >= Cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Card index is out of range");
            }

            Card card = Cards[index];
            Cards.RemoveAt(index);
            return card;
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
        }

    // Checks if the hand contains a card of a specific type, aiding in strategic gameplay.
        public bool HasCardOfType(Card.CardType cardType)
        {
            return Cards.Any(card => card.Type == cardType);
        }

    /// <summary>
    /// Checks if the hand contains a number card with the specified value
    /// </summary>
    /// <param name="value">The value to check for</param>
    /// <returns>True if the hand contains a number card with the specified value, false otherwise</returns>
    public bool HasNumberCard(int value)
        {
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

            // Returns a string representation of the hand, useful for debugging and display.
        public override string ToString()
        {
            if (Cards.Count == 0)
            {
                return "Empty hand";
            }

            return string.Join(", ", Cards.Select(card => card.ToString()));
        }

        // Saves the current state of the hand to a file for persistence.
        public void SaveHandToFile(string filePath)
        {
            HandWrapper wrapper = new HandWrapper { cards = Cards };
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
                Cards = wrapper.cards ?? new List<Card>();
            }
        }
        
    }
