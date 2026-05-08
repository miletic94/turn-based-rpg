public class BattleState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    public BattleState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public async void Enter()
    {
        var battleController = _context.BattleBootstrapper.Load();
        battleController.Initialize(_context.GameplayContext.Hero, _context.GameplayContext.CurrentEnemy);

        var result = await battleController.Run();

        if (result.Role == CombatantRole.Player)
            _gameplayStateMachine.EnterReward();
        else
            _gameplayStateMachine.EnterMap();
    }
    public void Exit()
    {
        _context.BattleBootstrapper.Unload();
    }
}