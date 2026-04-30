using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public async Awaitable SwitchState(IState newState)
    {
        if (_currentState != null)
            await _currentState.Exit();

        _currentState = newState;

        if (_currentState != null)
            await _currentState.Enter();
    }
}