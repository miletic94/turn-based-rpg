// using UnityEngine;

// public class RunState : IState
// {
//     private readonly ApplicationStateMachine _app;
//     private readonly RunFlowStateMachine _runFlow;

//     public RunState(ApplicationStateMachine app)
//     {
//         _app = app;
//     }

//     public async Awaitable Enter()
//     {
//         await SceneManager.LoadSceneAsync("RunScene");

//         var runContext = Object.FindFirstObjectByType<RunSceneContext>();

//         _runFlow = new RunFlowStateMachine(runContext);

//         await _runFlow.EnterMap();
//     }

//     public Awaitable Exit()
//     {
//         return Awaitable.CompletedTask;
//     }
// }