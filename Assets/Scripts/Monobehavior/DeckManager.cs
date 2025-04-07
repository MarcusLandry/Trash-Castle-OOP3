using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    private Deck deck;
    public Transform drawPileAnchor; // Optional: assign in inspector if you want drawn cards to spawn visually here

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        deck = new Deck();
    }

    public Card DrawCard()
    {
        Card drawn = deck.DrawCard();
        if (drawn != null)
        {
            Debug.Log("Drew card: " + drawn);
            // Optionally instantiate a card UI prefab here
        }
        else
        {
            Debug.LogWarning("Deck is empty!");
        }
        return drawn;
    }

}