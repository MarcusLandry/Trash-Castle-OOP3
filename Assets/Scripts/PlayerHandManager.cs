using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class PlayerHandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private SplineContainer splineContainer;

    [SerializeField] private Transform spawnPoint;

    private List<GameObject> cardHand = new();

    private void DrawCard()
    {
        
    }

    private void UpdateCardPosition()
    {
        if (cardHand.Count == 0) return;
        
        // Sets the spacing between cards in the hand.
        float cardSpacing = 1f / maxHandSize;
        
        // Starts the first card in the middle and
        // fans the hand out from there.
        float firstCardPosition = 0.5f - (cardHand.Count - 1) * cardSpacing / 2;
        
        Spline spline = splineContainer.Spline;

        // For loop to create the smooth card sliding movement on spine.
        for (int i = 0; i < cardHand.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluatePosition(p);
            Vector3 up = spline.EvaluatePosition(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            cardHand[i].transform.DOMove(splinePosition, 0.25f);
            cardHand[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }

    }
}
