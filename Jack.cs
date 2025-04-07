using System;

public class Jack : Card
{
   public Jack(Suit suit) : base(CardType.Jack, suit, null) {}

    public override bool PerformSpecialAction(GameState gameState)
    {

        Card bonusCard = gameState.DrawCard();

        if (bonusCard == null)
        {
            // no card drawn
            return false;
        }

        bonusCard.damage += 5;

        return true;
    }
}
