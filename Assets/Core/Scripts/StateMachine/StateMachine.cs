using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public void SwitchState(IState newState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = newState;

        if (_currentState != null)
            _currentState.Enter();
    }
}