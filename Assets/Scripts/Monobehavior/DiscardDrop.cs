using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardDrop : MonoBehaviour
{
public void OnCardDrop(PlayerHandManager card)
    {
        Destroy(card.gameObject);
    }
}
