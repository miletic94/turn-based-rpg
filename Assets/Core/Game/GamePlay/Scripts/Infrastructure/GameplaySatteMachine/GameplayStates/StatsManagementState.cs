using System.Collections.Generic;

public class StatsManagementState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    public StatsManagementState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public void Enter()
    {
        _context.StatsManagementBootstrapper.Load(_context.GameplayContext, OnSave);
    }

    public void Exit()
    {
        _context.StatsManagementBootstrapper.Unload();
    }

    private void OnSave()
    {
        _gameplayStateMachine.EnterMap();
    }
}