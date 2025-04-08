using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the overall state of the Trash Castle game,
/// including the deck, players, turn flow, and game rules.
/// </summary>
public class GameState
{
    public Deck Deck { get; private set; }
    public List<Hand> Players { get; private set; }

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
    /// gets a list of all active players excluding the current player
    /// </summary>
    public List<Hand> GetOpponents(Hand currentPlayer)
    {
        return Players.Where(p => p != currentPlayer && p.CastleHealth > 0).ToList();
    }

    /// <summary>
    /// selects a random opponent from current players
    /// </summary>
    public Hand GetRandomOpponent(Hand currentPlayer)
    {
        List<Hand> opponents = GetOpponents();

        if (opponents.Count == 0)
        {
            return null;
        }
            

        int index = Random.Range(0, opponents.Count);
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
}