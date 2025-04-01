using System;

public class King : Card
{
	 public King(Suit suit) : base(CardType.King, suit, null) { }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // Define King's special ability
        return true;
    }
}
