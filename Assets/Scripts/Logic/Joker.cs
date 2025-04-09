using System;
using System.Collections.Generic;
using System.Linq;

public class Joker : Card
{
    public Joker() : base(CardType.Joker, null, null) 
    { 
        Damage = 7;
    }

    public override bool PerformSpecialAction(GameState gameState)
    {
        foreach (Hand player in gameState.Players)
        {
            List<Card> cardsToReplace = player.Cards.ToList();

            if (cardsToReplace.Count == 0)
            {
                // Debug.Log($"{player.PlayerName} has no cards in their grid to reshuffle.");
                continue;
            }

            // Remove cards from players hand and add them back to the deck
            foreach (var card in cardsToReplace)
            {
                gameState.Deck.cards.Add(card);
                player.RemoveCard(card);
            }

            gameState.Deck.Shuffle();
            //Debug.Log($"{player.PlayerName}'s collection grid was cleared. Drawing {cardsToReplace} new cards.");

            for (int i = 0; i < cardsToReplace.Count; i++)
            {
                Card newCard = gameState.Deck.DrawCard();
                if (newCard != null)
                {
                    player.AddCard(newCard);
                }
            }
            
        }
        return true;
    }
}
