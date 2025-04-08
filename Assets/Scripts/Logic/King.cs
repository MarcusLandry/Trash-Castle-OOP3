using System;

public class King : Card
{
	 public King(Suit suit) : base(CardType.King, suit, null) 
     { 
        Damage = 0;
     }

    public override bool PerformSpecialAction(GameState gameState)
    {
        Hand currentPlayer = gameState.CurrentPlayer;

        // Discard the King (already in hand or just drawn)
        currentPlayer.RemoveCard(this);

        // Draw another card immediately
        Card bonusCard = gameState.Deck.DrawCard();
        if (bonusCard != null)
        {
            bonusCard.Damage += 5;
            currentPlayer.AddCard(bonusCard);
            return true;
        }

        // If no card could be drawn, the king has no effect
        return true;
    }
}
