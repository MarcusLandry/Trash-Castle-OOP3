using System;

public class Ace : Card
{
    public AceCard(Suit suit) : base(CardType.Ace, suit, null) { }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // Define Ace's special ability
        return true;
    }
}
