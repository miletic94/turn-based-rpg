using System;
using System.Collections.Generic;
using UnityEngine;


public class MoveView : MonoBehaviour, IMoveView
{
    [SerializeField] MoveElementView moveItemViewPrefab;
    [SerializeField] Transform content;
    public void ShowMoves(List<Move> moves, Action<Move> onMoveSelected, Action<Move> onHoverDelayed)
    {
        // TODO: Implement pooling for move buttons
        foreach (Transform child in content)
            Destroy(child.gameObject);

        // Instantiate new buttons
        foreach (var move in moves)
        {
            var moveItemView = Instantiate(moveItemViewPrefab, content);

            moveItemView
                .SetInteractable(true)
                .SetLabel(move.Name)
                .MakeClickable(() => onMoveSelected(move))
                .MakeHoverable(onHoverDelayed: () => onHoverDelayed(move));
        }
    }
}