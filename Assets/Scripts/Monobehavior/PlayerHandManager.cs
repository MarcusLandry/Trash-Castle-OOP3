using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class PlayerHandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform spawnPoint;

    private List<GameObject> cardHand = new();
    private Collider2D col;
    private Vector3 startDragPosition;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down on Card");
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        Debug.Log("Dragging Card");
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up on Card");
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        // Check if the card is dropped on a valid drop area
        if (hitCollider != null && hitCollider.TryGetComponent(out ICardDropArea cardDropArea))
        {
            cardDropArea.OnCardDrop(this);
        }
        else
        {
            // Return to the starting position if not dropped on a valid area
            transform.position = startDragPosition;
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DrawCard();
    }

    private void DrawCard()
    {
        if (cardHand.Count >= maxHandSize) return;
        GameObject g = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        cardHand.Add(g);
        UpdateCardPosition();
    }

    private void UpdateCardPosition()
    {
        if (cardHand.Count == 0) return;

        // Sets the spacing between cards in the hand.
        float cardSpacing = 1f / maxHandSize;

        // Starts the first card in the middle and fans the hand out from there.
        float firstCardPosition = 0.5f - (cardHand.Count - 1) * cardSpacing / 2;

        Spline spline = splineContainer.Spline;

        // For loop to create the smooth card sliding movement on spine.
        for (int i = 0; i < cardHand.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 tangent = spline.EvaluateTangent(p);

            // Calculate the rotation to make cards face the player
            Vector3 forward = -Vector3.forward; // Points toward the player
            Vector3 right = Vector3.Cross(Vector3.up, tangent).normalized;
            Vector3 up = Vector3.Cross(tangent, right).normalized;

            // Create rotation that makes the card face the player while following the spline curve
            Quaternion rotation = Quaternion.LookRotation(forward, up);

            // Animate the card to its new position and rotation
            cardHand[i].transform.DOMove(splinePosition, 0.25f);
            cardHand[i].transform.DORotateQuaternion(rotation, 0.25f);
        }
    }
}
