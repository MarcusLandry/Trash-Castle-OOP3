using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardDrop : MonoBehaviour
{
    public void OnCardDrop(PlayerHandManager card)
    {
        // Check if the card is valid before dropping
        if (card != null)
        {
            card.transform.position = transform.position;
            Debug.Log("Played this card");
        }
        else
        {
            Debug.LogWarning("Invalid card dropped.");
        }
    }
}
