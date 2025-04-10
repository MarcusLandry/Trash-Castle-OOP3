using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour that serves as the Unity-facing controller for the Trash Castle game.
/// Manages player input, UI updates, and interaction with GameState logic.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState GameState { get; private set; }

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        GameState = new GameState();
        Debug.Log("Game Initialized");
        Debug.Log($"Starting Player: {GameState.CurrentPlayer.PlayerName}");
    }

    public void DrawCardForCurrentPlayer()
    {
        GameState.DrawCardForCurrentPlayer();
    }

    public void EndTurn()
    {
        GameState.EndTurn();
    }

    public void UseCard(Card card)
    {
        bool actionSuccess = card.PerformSpecialAction(GameState);
        if (actionSuccess)
        {
            GameState.CurrentPlayer.RemoveCard(card);
            Debug.Log($"{GameState.CurrentPlayer.PlayerName} used {card.Name}.");
        }
        else
        {
            Debug.LogWarning($"{card.Name} action failed.");
        }
    }

    public void DealDamageToOpponent(int amount)
    {
        var opponent = GameState.GetRandomOpponent(GameState.CurrentPlayer);
        if (opponent != null)
        {
            GameState.DealDamage(opponent, amount);
        }
    }
}
