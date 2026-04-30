using UnityEngine;

public class AsyncStateMachine
{
    private IAsyncState _currentState;

    public async Awaitable SwitchState(IAsyncState newState)
    {
        if (_currentState != null)
            await _currentState.Exit();

        _currentState = newState;

        if (_currentState != null)
            await _currentState.Enter();
    }
}