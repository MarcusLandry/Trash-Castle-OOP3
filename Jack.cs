using System;

public class Jack : Card
{
   public Jack(Suit suit) : base(CardType.Jack, suit, null) { }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // Define Jack's special ability
        return true;
    }
}
