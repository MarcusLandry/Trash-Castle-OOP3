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
        Hand opponent = gameState.GetRandomOpponent(currentPlayer);

        if (opponent == null)
        {
            // Debug.LogWarning("Jack tried to steal but no valid opponents.");
            return false;
        }

        if (opponent.Cards.Count == 0)
        {
            // Debug.Log($"{opponent.PlayerName} has no cards to steal.");
            return false;
        }

        // Steal a random card
        int randomCardIndex = UnityEngine.Random.Range(0, opponent.Cards.Count);
        Card stolenCard = opponent.Cards[randomCardIndex];

        opponent.RemoveCard(stolenCard);
        currentPlayer.AddCard(stolenCard);

        // Debug.Log($"{currentPlayer.PlayerName} used Jack to steal {stolenCard.Name} from {opponent.PlayerName}.");

        return true;
    }
}
