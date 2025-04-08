using System;
using System.Collections.Generic;
using System.Linq;


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
        
    }
