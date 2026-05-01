using UnityEngine;

public class MapState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    public MapState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public void Enter()
    {
        _context.MapBootstrapper.InitializeAndRun(_gameplayStateMachine);

        // var result =
        //     await _context.MapBootstrapper.WaitForSelection();

        // if (result.OpenMoveManagement)
        // {
        //     await _gameplayStateMachine.EnterMoveManagement();
        //     return;
        // }

        // await _gameplayStateMachine.EnterBattle(result.SelectedEncounter);
    }

    public void Exit()
    {
        _context.MapBootstrapper.Unload();
    }
}