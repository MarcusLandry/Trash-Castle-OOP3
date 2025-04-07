using System;
using System.Collections.Generic;
using System.Linq;


    /// <summary>
    /// Represents a player's hand of cards in the Trash Castle game.
    /// Manages the cards a player currently holds and provides methods to add, remove, and play cards.
    /// </summary>
    public class Hand
    {
        // The maximum number of cards a player can hold
        private readonly int _maxHandSize;
        
        // The collection of cards in the player's hand
        private List<Card> _cards;
        
        /// <summary>
        /// Gets the number of cards currently in the hand
        /// </summary>
        public int CardCount => _cards.Count;
        
        /// <summary>
        /// Gets a read-only view of the cards in the hand
        /// </summary>
        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();
        
        /// <summary>
        /// Initializes a new instance of the Hand class with the specified maximum hand size
        /// </summary>
        /// <param name="maxHandSize">The maximum number of cards this hand can hold</param>
        public Hand(int maxHandSize = 10)
        {
            _maxHandSize = maxHandSize;
            _cards = new List<Card>();
        }
        
        /// <summary>
        /// Adds a card to the hand if there is room
        /// </summary>
        /// <param name="card">The card to add</param>
        /// <returns>True if the card was added successfully, false if the hand is full</returns>
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
        
        /// <summary>
        /// Adds multiple cards to the hand, up to the maximum hand size
        /// </summary>
        /// <param name="cards">The cards to add</param>
        /// <returns>The number of cards that were successfully added</returns>
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
        
        /// <summary>
        /// Removes a card from the hand by index
        /// </summary>
        /// <param name="index">The index of the card to remove</param>
        /// <returns>The removed card</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range</exception>
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
        
        /// <summary>
        /// Removes a specific card from the hand
        /// </summary>
        /// <param name="card">The card to remove</param>
        /// <returns>True if the card was found and removed, false otherwise</returns>
        public bool RemoveCard(Card card)
        {
            return _cards.Remove(card);
        }
        
        /// <summary>
        /// Gets a card at a specific index without removing it
        /// </summary>
        /// <param name="index">The index of the card to get</param>
        /// <returns>The card at the specified index</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range</exception>
        public Card GetCard(int index)
        {
            if (index < 0 || index >= _cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Card index is out of range");
            }
            
            return _cards[index];
        }
        
        /// <summary>
        /// Checks if the hand contains a specific card
        /// </summary>
        /// <param name="card">The card to check for</param>
        /// <returns>True if the hand contains the card, false otherwise</returns>
        public bool HasCard(Card card)
        {
            return _cards.Contains(card);
        }
        
        /// <summary>
        /// Checks if the hand contains a card of a specific type
        /// </summary>
        /// <param name="cardType">The type of card to check for</param>
        /// <returns>True if the hand contains a card of the specified type, false otherwise</returns>
        public bool HasCardOfType(Card.CardType cardType)
        {
            return _cards.Any(card => card.Type == cardType);
        }
        
        /// <summary>
        /// Checks if the hand contains a number card with the specified value
        /// </summary>
        /// <param name="value">The value to check for</param>
        /// <returns>True if the hand contains a number card with the specified value, false otherwise</returns>
        public bool HasNumberCard(int value)
        {
            return _cards.Any(card => card.Type == Card.CardType.Number && card.Value == value);
        }
        
        /// <summary>
        /// Gets all the number cards in the hand
        /// </summary>
        /// <returns>A list of number cards</returns>
        public List<Card> GetNumberCards()
        {
            return _cards.Where(card => card.Type == Card.CardType.Number).ToList();
        }
        
        /// <summary>
        /// Gets all the special cards in the hand
        /// </summary>
        /// <returns>A list of special cards</returns>
        public List<Card> GetSpecialCards()
        {
            return _cards.Where(card => card.IsSpecialCard).ToList();
        }
        
        /// <summary>
        /// Plays a card from the hand (removes it and returns it)
        /// </summary>
        /// <param name="card">The card to play</param>
        /// <returns>The played card, or null if the card is not in the hand</returns>
        public Card PlayCard(Card card)
        {
            int index = _cards.IndexOf(card);
            
            if (index == -1)
            {
                return null; // Card not found in hand
            }
            
            return RemoveCardAt(index);
        }
        
        /// <summary>
        /// Plays a card of a specific type if available
        /// </summary>
        /// <param name="cardType">The type of card to play</param>
        /// <returns>The played card, or null if no card of that type is in the hand</returns>
        public Card PlayCardOfType(Card.CardType cardType)
        {
            Card card = _cards.FirstOrDefault(c => c.Type == cardType);
            
            if (card != null)
            {
                RemoveCard(card);
            }
            
            return card;
        }
        
        /// <summary>
        /// Plays a number card with the specified value if available
        /// </summary>
        /// <param name="value">The value of the number card to play</param>
        /// <returns>The played card, or null if no card with that value is in the hand</returns>
        public Card PlayNumberCard(int value)
        {
            Card card = _cards.FirstOrDefault(c => c.Type == Card.CardType.Number && c.Value == value);
            
            if (card != null)
            {
                RemoveCard(card);
            }
            
            return card;
        }
        
        /// <summary>
        /// Clears all cards from the hand
        /// </summary>
        /// <returns>The list of cards that were in the hand</returns>
        public List<Card> ClearHand()
        {
            List<Card> cards = new List<Card>(_cards);
            _cards.Clear();
            return cards;
        }
        
        /// <summary>
        /// Returns a string representation of the hand
        /// </summary>
        /// <returns>A string describing the cards in the hand</returns>
        public override string ToString()
        {
            if (_cards.Count == 0)
            {
                return "Empty hand";
            }
            
            return string.Join(", ", _cards.Select(card => card.ToString()));
        }
    }
