using System;

public class Queen : Card
{
    public Queen(Suit suit) : base(CardType.Queen, suit, null) 
    { 
        Damage = 4;
    }

    public override bool PerformSpecialAction(GameState gameState)
    {
        // Define Queen's special ability
        Hand currentPlayer = gameState.CurrentPlayer;

        currentPlayer.Castle += 5;
        return true;
    }
}
