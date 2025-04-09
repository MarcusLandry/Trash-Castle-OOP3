using System;
public class NumberCard : Card
{
    public NumberCard(Suit suit, int value) : base(CardType.Number, suit, value)
    {
        // Any extra number-card-specific logic could go here
    }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // NumberCard's don't have special actions
        return false;
    }
}