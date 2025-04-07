using system;
using System.Collections.Generic;
public class GameState
{
    public Deck Deck { get; set; }
    public List<Card> { get; set; }

    public GameState()
    {
        Deck = new Deck();
        DiscardPile = new List<Card>();
    }

    public Card DrawCard()
    {
        return Deck.DrawCard();
    }

}