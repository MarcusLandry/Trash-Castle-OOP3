using System;
using System.Collections.Generic;

    /// <summary>
    /// Base class for all cards in the Trash Castle game.
    /// Provides common properties and methods that all cards share.
    /// </summary>
    public abstract class Card
    {
        // Enum for card suits
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        // Enum for card types
        public enum CardType
        {
            Number,
            Jack,
            Queen,
            King,
            Ace,
            Joker
        }

        // Core card properties
        public CardType Type { get; protected set; }
        public Suit? CardSuit { get; protected set; } // Nullable because Joker has no suit
        public int? Value { get; protected set; } // Nullable because special cards might not have a numeric value
        public string Name { get; protected set; }
        public bool IsSpecialCard { get; protected set; }

        /* public int Damage {protected set;} */

        // Constructor
        protected Card(CardType type, Suit? suit, int? value)
        {
            Type = type;
            CardSuit = suit;
            Value = value;
            IsSpecialCard = type != CardType.Number;

           /*  Damage = 0;

            if (Type == CardType.Number && Value.HasValue)
            {
                // According to game rules: lower cards (2-5) deal 2 damage, higher cards (6-10) deal 4 damage
                Damage = Value.Value <= 5 ? 2 : 4;
            } */


            GenerateName();
        }

        // Generate the name of the card based on its properties
        private void GenerateName()
        {
            if (!CardSuit.HasValue && Type == CardType.Joker)
            {
                Name = "Joker";
                return;
            }

            string valueStr;
            switch (Type)
            {
                case CardType.Jack:
                    valueStr = "Jack";
                    break;
                case CardType.Queen:
                    valueStr = "Queen";
                    break;
                case CardType.King:
                    valueStr = "King";
                    break;
                case CardType.Ace:
                    valueStr = "Ace";
                    break;
                default:
                    valueStr = Value.ToString();
                    break;
            }

            Name = $"{valueStr} of {CardSuit}";
        }

        // Methods that can be overridden by specific card types
        
        /// <summary>
        /// Gets the damage value of the card for the Battle Phase
        /// </summary>
        /// <returns>The amount of damage this card deals</returns>
        public virtual int GetDamageValue()
        {
            // Base implementation for number cards
            if (Type == CardType.Number && Value.HasValue)
            {
                // According to game rules: lower cards (2-5) deal 2 damage, higher cards (6-10) deal 4 damage
                return Value.Value <= 5 ? 2 : 4;
            }
            
            // Default value for special cards - to be overridden
            return 0;
        }

        /// <summary>
        /// Abstract method to be implemented by each card type to define its special action
        /// </summary>
        /// <param name="gameState">Reference to the current game state</param>
        /// <returns>True if the action was successful, false otherwise</returns>
        public abstract bool PerformSpecialAction(GameState gameState);

        /// <summary>
        /// Determines if the card can be placed in the collection phase at a specific position
        /// </summary>
        /// <param name="position">Position in the collection (1-10)</param>
        /// <param name="gameState">Reference to the current game state</param>
        /// <returns>True if the card can be placed, false otherwise</returns>
        public virtual bool CanBePlacedInCollection(int position, GameState gameState)
        {
            // Number cards can only be placed in their corresponding position
            if (Type == CardType.Number && Value.HasValue)
            {
                return position == Value.Value;
            }
            
            // Special cards have their own placement rules to be defined in derived classes
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }