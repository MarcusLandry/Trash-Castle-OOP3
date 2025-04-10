using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class GameStateWrapper
{
    public Deck Deck;
    public List<Card> DiscardPile;
}

/// <summary>
/// Manages the overall state of the Trash Castle game,
/// including the deck, players, turn flow, and game rules.
/// </summary>
public class GameState
{

    public Deck Deck { get; private set; }
    public List<Hand> Players { get; private set; }

    public List<Card> DiscardPile { get; private set; } = new List<Card>();

    private int currentPlayerIndex = 0;

    public Hand CurrentPlayer => Players[currentPlayerIndex];

    private void Start()
    {
        InitializeGame();
    }

    /// <summary>
    /// Sets up the deck and player hands.
    /// </summary>
    private void InitializeGame()
    {
        Deck = new Deck();

        Players = new List<Hand>
        {
            new Hand("Player 1"),
            new Hand("Player 2") // You can add more players as needed
        };

        // Optional: give starting hands
        for (int i = 0; i < 5; i++)
        {
            foreach (var player in Players)
            {
                DrawCardForPlayer(player);
            }
        }
    }

    public void AutoFillCollection(Hand player)
    {
        while (player.CollectionGrid.Count < 9) // 2–10 = 9 cards
        {
            Card card = Deck.DrawCard();
            if (card == null)
            {
                Debug.Log("Deck is empty.");
                break;
            }

            // Only keep number cards the player doesn't already have
            if (card.Type == Card.CardType.Number && card.Value.HasValue)
            {
                if (!player.HasNumberInCollection(card.Value.Value))
                {
                    player.AddToCollection(card);
                }
                else
                {
                    Debug.Log($"Duplicate number {card.Value.Value} discarded.");
                }
            }
            else
            {
                Debug.Log($"Special card {card.Name} drawn — not added to collection grid.");
                player.AddCard(card); // Or discard it, or queue it for Battle Phase
            }
        }
    }

    /// <summary>
    /// Draws a card for the current player.
    /// </summary>
    public void DrawCardForCurrentPlayer()
    {
        DrawCardForPlayer(CurrentPlayer);
    }

    /// <summary>
    /// Draws a card for a specific player.
    /// </summary>
    public void DrawCardForPlayer(Hand player)
    {
        Card card = Deck.DrawCard();
        if (card != null)
        {
            player.AddCard(card);
        }
        else
        {
            Debug.Log("Deck is empty!");
        }
    }

    /// <summary>
    /// Deals damage to a player's castle and logs the result.
    /// </summary>
    /// <param name="target">The player taking damage</param>
    /// <param name="amount">Amount of damage to deal</param>
    public void DealDamage(Hand target, int amount)
    {
        int previousHP = target.Castle;
        target.Castle -= amount;
        if (target.Castle < 0)
            target.Castle = 0;

        // Debug.Log($"{target.PlayerName} took {amount} damage! Castle: {previousHP} → {target.Castle}");

        // Optional: trigger defeat state
        if (target.Castle == 0)
        {
            Debug.Log($"{target.PlayerName}'s castle has been destroyed!");
        }
    }

    /// <summary>
    /// gets a list of all active players excluding the current player
    /// </summary>
    public List<Hand> GetOpponents(Hand currentPlayer)
    {
        return Players.Where(p => p != currentPlayer && p.Castle > 0).ToList();
    }
    
    /// <summary>
    /// selects a random opponent from current players
    /// </summary>
    public Hand GetRandomOpponent(Hand currentPlayer)
    {
        List<Hand> opponents = GetOpponents(currentPlayer);

        if (opponents.Count == 0)
        {
            return null;
        }
            

        int index = UnityEngine.Random.Range(0, opponents.Count);
        return opponents[index];
    }

    /// <summary>
    /// Moves to the next player's turn.
    /// </summary>
    public void EndTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
        Debug.Log($"It is now {CurrentPlayer.PlayerName}'s turn.");
    } 
    
    public void Discard(Card card)
    {
        DiscardPile.Add(card);
    }

    // Discards a card to the discard pile, which is necessary for managing played cards.
   
    // Saves the current game state to a file for persistence, allowing players to resume later.
    public void SaveGameStateToFile(string filePath)
    {
        GameStateWrapper wrapper = new GameStateWrapper { Deck = Deck, DiscardPile = DiscardPile };
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
            Deck = wrapper.Deck;
            DiscardPile = wrapper.DiscardPile ?? new List<Card>();
        }
    }
}
