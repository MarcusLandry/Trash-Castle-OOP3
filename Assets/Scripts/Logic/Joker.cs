using System;

public class Joker : Card
{
    public Joker() : base(CardType.Joker, null, null) { }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // Define Joker’s special ability
        return true;
    }
}
