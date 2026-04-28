using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveView : MonoBehaviour, IMoveView
{
    [SerializeField] GameObject moveButtonPrefab;
    [SerializeField] Transform content;
    private Action<Move> _onMoveSelected;
    private bool _isActive;

    public void SetMoves(List<Move> moves)
    {
        // Clear old buttons
        foreach (Transform child in content)
            Destroy(child.gameObject);

        // Instantiate new buttons
        foreach (var move in moves)
        {
            var buttonObj = Instantiate(moveButtonPrefab, content);
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<TMP_Text>();
            text.text = move.Name;

            button.onClick.AddListener(() => _onMoveSelected?.Invoke(move));
        }
    }

    public void EnableInput(Action<Move> onMoveSelected)
    {
        _onMoveSelected = onMoveSelected;
        _isActive = true;
    }

    public void DisableInput()
    {
        _onMoveSelected = null;
        _isActive = false;
    }

    public void OnMoveButtonClicked(Move move)
    {
        if (!_isActive) return;
        _onMoveSelected?.Invoke(move);
    }
}