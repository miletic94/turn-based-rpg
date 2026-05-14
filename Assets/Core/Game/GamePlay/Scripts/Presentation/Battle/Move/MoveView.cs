using System;
using System.Collections.Generic;
using UnityEngine;


public class MoveView : MonoBehaviour, IMoveView
{
    [SerializeField] MoveElementView moveItemViewPrefab;
    [SerializeField] Transform content;
    public void ShowMoves(List<Move> moves, Action<Move> onMoveSelected, Action<Move> onMoveHovered)
    {
        // TODO: Implement pooling for move buttons
        foreach (Transform child in content)
            Destroy(child.gameObject);

        // Instantiate new buttons
        foreach (var move in moves)
        {
            var moveItemView = Instantiate(moveItemViewPrefab, content);

            moveItemView
                .SetText(move.Name)
                .OnClicked(() => onMoveSelected(move))
                .OnHoverDelayed(() => onMoveHovered(move));
        }
    }
}