using System;

public class King : Card
{
	 public King(Suit suit) : base(CardType.King, suit, null) 
     { 
        Damage = 0;
     }

    public override bool PerformSpecialAction(GameState gameState)
    {
        Hand currentPlayer = gameState.CurrentPlayer

        // Discard the King (already in hand or just drawn)
        currentPlayer.DiscardCard(this);

        // Draw another card immediately
        Card bonusCard = gameState.DrawCard();
        if (bonusCard != null)
        {
            currentPlayer.AddToHand(bonusCard);

            int totalDamage = this.Damage + 5 + bonusCard.GetDamageValue();
            gameState.DealDamage(opponent, totalDamage);

            Debug.Log($"King used Royal Fortune! Drew {bonusCard.Name} and dealt {totalDamage} total damage.");
            return true;
        }

        // If no card could be drawn, still deal only 2 damage
        Debug.Log("King used Royal Fortune, but no card to draw. The King is useless!");
        return true;
    }
}
