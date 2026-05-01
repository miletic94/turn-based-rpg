using UnityEngine;

public class BattleState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;
    private readonly Character _player;
    private readonly Character _enemy;
    public BattleState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context,
        Character player,
        Character enemy
        )
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context; ;
        _player = player;
        _enemy = enemy;
    }

    public async void Enter()
    {

        var result = await _context.BattleBootstrapper.InitializeAndRun(_player, _enemy);

        if (result.Role == CombatantRole.Player)
            _gameplayStateMachine.EnterReward(); // await _flow.EnterReward(result.LearnedMove)
        else
            _gameplayStateMachine.EnterMap();
    }

    public void Exit()
    {
        _context.BattleBootstrapper.Unload();
    }
}