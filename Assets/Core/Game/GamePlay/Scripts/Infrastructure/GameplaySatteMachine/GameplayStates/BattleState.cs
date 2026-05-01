using System.Collections.Generic;
using UnityEngine;

public class BattleState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    private readonly Character _playerCharacter;
    private readonly Character _enemyCharacter;

    public BattleState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context,
        Character player,
        Character enemy)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;

        _playerCharacter = player;
        _enemyCharacter = enemy;
    }

    public async void Enter()
    {
        var (player, enemy) = _context.BattleBootstrapper.Initialize(_playerCharacter, _enemyCharacter);

        var result = await _context.BattleBootstrapper.Run(player, enemy);

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