using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    private readonly IMoveView _moveView;

    public MoveController(IMoveView moveView)
    {
        _moveView = moveView;
    }

    /// Show available moves
    public void ShowMoves(List<Move> moves)
    {
        _moveView.ShowMoves(moves);
    }

    /// Ask player to pick a move
    public Awaitable<Move> WaitForPlayerMove(Character character)
    {
        var tcs = new AwaitableCompletionSource<Move>();

        _moveView.ShowMoves(character.Moves);

        _moveView.EnableInput(move =>
        {
            _moveView.DisableInput();     // prevent double click
            tcs.SetResult(move);     // resolve async
        });

        return tcs.Awaitable;
    }
}

