using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleView : MonoBehaviour
{
    [Header("Moves")]
    [SerializeField] private MoveView _moveViewPrefab;
    [SerializeField] private Transform _movesContainer;

    public void Initialize(List<Move> moves, Action<Move> onMoveSelected)
    {
        ShowMoves(moves, onMoveSelected);
    }

    public void ShowMoves(List<Move> moves, Action<Move> onMoveSelected)
    {
        // TODO: Implement pooling for move buttons
        foreach (Transform child in _movesContainer)
            Destroy(child.gameObject);

        // Instantiate new buttons
        foreach (var move in moves)
        {
            var moveItemView = Instantiate(_moveViewPrefab, _movesContainer);

            moveItemView
                .SetLabel(move.Name)
                .MakeClickable(() => onMoveSelected(move));
        }
    }

}