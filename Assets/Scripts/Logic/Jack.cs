using System;

public class Jack : Card
{
   public Jack(Suit suit) : base(CardType.Jack, suit, null) 
    {
        Damage = 2;
    }

    public override bool PerformSpecialAction(GameState gameState)
    {
        Hand currentPlayer = gameState.CurrentPlayer;
        ponents = gameState.GetRandomOpponent(currentPlayer);

        if (opponents.Count == 0)
        {
            Debug.LogWarning("Jack tried to steal but no valid opponents.");
            return false;
        }

        // Pick a random opponent
        int index = UnityEngine.Random.Range(0, opponents.Count);
        Hand targetOpponent = opponents[index];

        if (targetOpponent.Cards.Count == 0)
        {
            Debug.Log($"{targetOpponent.PlayerName} has no cards to steal.");
            return false;
        }

        // Steal a random card
        int randomCardIndex = UnityEngine.Random.Range(0, targetOpponent.Cards.Count);
        Card stolenCard = targetOpponent.Cards[randomCardIndex];

        targetOpponent.RemoveCard(stolenCard);
        currentPlayer.AddCard(stolenCard);

        Debug.Log($"{currentPlayer.PlayerName} used Jack to steal {stolenCard.Name} from {targetOpponent.PlayerName}.");

        // Optional log
        gameState.Logger?.LogMove(currentPlayer.PlayerName, "Steal (Random)", "Jack", 0);

        return true;
    }
}
