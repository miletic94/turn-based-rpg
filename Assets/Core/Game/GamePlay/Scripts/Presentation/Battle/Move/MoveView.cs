using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveView : MonoBehaviour, IMoveView
{
    [SerializeField] GameObject moveButtonPrefab;
    [SerializeField] Transform content;
    public void ShowMoves(List<Move> moves, Action<Move> onMoveSelected)
    {
        // TODO: Implement pooling for move buttons
        foreach (Transform child in content)
            Destroy(child.gameObject);

        // Instantiate new buttons
        foreach (var move in moves)
        {
            var buttonObj = Instantiate(moveButtonPrefab, content);
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<TMP_Text>();
            text.text = move.Name;

            button.onClick.AddListener(() => onMoveSelected(move));
        }
    }
}