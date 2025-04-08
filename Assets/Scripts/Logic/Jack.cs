using System;
using Unity;

public class Jack : Card
{
   public Jack(Suit suit) : base(CardType.Jack, suit, null) 
    {
        Damage = 2;
    }

    public override bool PerformSpecialAction(GameState gameState)
    {
        Hand currentPlayer = gameState.CurrentPlayer;
        opponent = gameState.GetRandomOpponent(currentPlayer);

        if (opponent == null)
        {
            Debug.LogWarning("Jack tried to steal but no valid opponents.");
            return false;
        }

        if (opponent.Cards.Count == 0)
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

        return true;
    }
}
