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
        var mapController = _context.MapBootstrapper.Load();
        mapController.Initialize(_context.GameplayContext.Enemies,
            OnEnemySelected,
            OnManageMovesButtonClicked);
    }

    public void Exit()
    {
        _context.MapBootstrapper.Unload();
    }

    private void OnManageMovesButtonClicked()
    {
        _gameplayStateMachine.EnterMoveManagement();
    }

    private void OnEnemySelected(Character selectedEnemy)
    {
        _context.GameplayContext.CurrentEnemy = selectedEnemy;
        _gameplayStateMachine.EnterBattle();
    }
}