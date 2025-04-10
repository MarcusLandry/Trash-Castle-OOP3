using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameStateWrapper
{
    public Deck MainDeck;
    public List<Card> DiscardPile;
}

public class GameState
{
    public Deck MainDeck { get; private set; }
    public List<Card> DiscardPile { get; private set; }

    // Constructor initializes the game state with a deck and an empty discard pile.
    public GameState(Deck deck)
    {
        MainDeck = deck;
        DiscardPile = new List<Card>();
    }

    // Discards a card to the discard pile, which is necessary for managing played cards.
    public void Discard(Card card)
    {
        DiscardPile.Add(card);
    }

    // Saves the current game state to a file for persistence, allowing players to resume later.
    public void SaveGameStateToFile(string filePath)
    {
        GameStateWrapper wrapper = new GameStateWrapper { MainDeck = MainDeck, DiscardPile = DiscardPile };
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

    // Loads the game state from a file, enabling players to continue from a saved point.
    public void LoadGameStateFromFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            GameStateWrapper wrapper = JsonUtility.FromJson<GameStateWrapper>(json);
            MainDeck = wrapper.MainDeck;
            DiscardPile = wrapper.DiscardPile ?? new List<Card>();
        }
    }
}
