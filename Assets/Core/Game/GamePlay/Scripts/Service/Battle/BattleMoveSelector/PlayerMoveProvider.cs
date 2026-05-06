using UnityEngine;

public class PlayerMoveProvider : IMoveProvider
{
    private AwaitableCompletionSource<Move> _pendingRequest;
    private Combatant _currentActor;

    public Awaitable<Move> GetMove(Combatant actor)
    {
        _currentActor = actor;
        _pendingRequest = new AwaitableCompletionSource<Move>();

        return _pendingRequest.Awaitable;
    }

    public void OnMoveSelected(Move move)
    {
        if (_pendingRequest == null)
            return;

        _pendingRequest.SetResult(move);
        _pendingRequest = null;
    }
}