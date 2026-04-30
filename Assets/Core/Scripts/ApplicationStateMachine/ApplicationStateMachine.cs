using UnityEngine;

public class ApplicationStateMachine
{
    private readonly AsyncStateMachine _machine = new();

    public Awaitable EnterMainMenu()
        => _machine.SwitchState(new MainMenuState(this));

    public Awaitable EnterGameplay()
        => _machine.SwitchState(new GameplayState(this));
}