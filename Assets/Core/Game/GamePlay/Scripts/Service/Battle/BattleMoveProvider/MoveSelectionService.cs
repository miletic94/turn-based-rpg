using UnityEngine;

public class MoveSelectionService
{
    private AwaitableCompletionSource<Move> _pendingRequest;

    public Awaitable<Move> RequestMove()
    {
        _pendingRequest = new AwaitableCompletionSource<Move>();

        return _pendingRequest.Awaitable;
    }

    public void SelectMove(Move move)
    {
        if (_pendingRequest == null)
            return;
        _pendingRequest.SetResult(move);
        _pendingRequest = null;
    }
}