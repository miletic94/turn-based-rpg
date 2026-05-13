using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveView : MonoBehaviour, IMoveView
{
    [SerializeField] MoveButtonUI moveButtonPrefab;
    [SerializeField] Transform content;
    public void ShowMoves(List<Move> moves, Action<Move> onMoveSelected, Action<Move> onMoveHovered)
    {
        // TODO: Implement pooling for move buttons
        foreach (Transform child in content)
            Destroy(child.gameObject);

        // Instantiate new buttons
        foreach (var move in moves)
        {
            var moveButtonUI = Instantiate(moveButtonPrefab, content);
            moveButtonUI.SetText(move.Name);

            moveButtonUI.SetOnClick(() => onMoveSelected(move));
            moveButtonUI.SetOnHover(() => onMoveHovered(move));
        }
    }
}