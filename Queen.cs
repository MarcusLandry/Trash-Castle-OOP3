using System;

public class Queen : Card
{
    public Queen(Suit suit) : base(CardType.Queen, suit, null) { }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // Define Queen's special ability
        return true;
    }
}
