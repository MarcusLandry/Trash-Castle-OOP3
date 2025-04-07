using System;

public class King : Card
{
	 public King(Suit suit) : base(CardType.King, suit, null) { }

    public override bool PerformSpecialAction(GameState gameState)
    {		
        Card bonusCard = gameState.DrawCard();

        if (bonusCard == null)
        {
            // no card drawn
            return false;
        }

        bonusCard.damage += 5;

        return bonusCard;
    }
}
