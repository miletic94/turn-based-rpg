using System;
using UnityEngine;

public class BattleView : MonoBehaviour, IBattleView
{
    private Action<Move> _callback;

    public void ShowMoveSelection(BattleState state, Action<Move> onMoveSelected)
    {
        _callback = onMoveSelected;
    }

    public void OnMoveButtonClicked(Move move)
    {
        gameObject.SetActive(false);
        _callback?.Invoke(move);
    }
}