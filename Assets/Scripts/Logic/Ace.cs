using System;

public class Ace : Card
{
    public Ace(Suit suit) : base(CardType.Ace, suit, null) 
    {
        Damage = 1;
    }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // Define Ace's special ability
        int cardAmount = GameState.CurrentPlayer.Cards.Count;
        this.Damage = cardAmount * Damage;
        return true;
    }
}
