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

    public GameState(Deck deck)
    {
        MainDeck = deck;
        DiscardPile = new List<Card>();
    }

    public void Discard(Card card)
    {
        DiscardPile.Add(card);
    }

    public void SaveGameStateToFile(string filePath)
    {
        GameStateWrapper wrapper = new GameStateWrapper { MainDeck = MainDeck, DiscardPile = DiscardPile };
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

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
