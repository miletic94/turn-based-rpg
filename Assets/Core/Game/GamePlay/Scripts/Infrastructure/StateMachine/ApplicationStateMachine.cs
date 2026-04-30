// using UnityEngine;

// public class ApplicationStateMachine
// {
//     private readonly StateMachine _machine = new();

//     public Awaitable EnterBoot()
//         => _machine.SwitchState(new BootState(this));

//     public Awaitable EnterMainMenu()
//         => _machine.SwitchState(new MainMenuState(this));

//     public Awaitable EnterRun()
//         => _machine.SwitchState(new RunState(this));
// }