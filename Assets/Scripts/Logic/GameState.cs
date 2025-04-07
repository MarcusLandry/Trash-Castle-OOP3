using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class GameState
{
    
    public Deck MainDeck { get; private set; }
    public List<Card> DiscardPile { get; private set; }

    public Phase CurrentPhase { get; set; }

    public enum Phase
    {
        Draw,
        Collection,
        Battle
    }

    public GameState(Deck deck)
    {
        MainDeck = deck;
        DiscardPile = new List<Card>();
        CurrentPhase = Phase.Draw;
    }

    public void Discard(Card card)
    {
        DiscardPile.Add(card);
    }

    // Maybe a method to transition phases, track victory, etc.
}