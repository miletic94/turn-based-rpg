using UnityEngine;

public class XpState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    public XpState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public async void Enter()
    {
        var xpController = _context.XpBootstrapper.Load(_context.GameplayContext.Hero.Xp);
        var result = await xpController.Initialize();
        if (result.RewardPoints > 0)
        {
            _context.GameplayContext.Hero.AddAvailableStatPoints(result.RewardPoints);
        }

        if (_context.GameplayContext.Hero.AvailableStatPoints > 0)
        {
            _gameplayStateMachine.EnterStatsManagement();
        }
        else
        {
            _gameplayStateMachine.EnterMap();
        }
    }

    public void Exit()
    {
        _context.XpBootstrapper.Unload();
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