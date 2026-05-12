using System.Collections.Generic;
using UnityEngine;

public class BattleController
{
    private BattleService _battleService;
    private readonly MoveController _moveController;
    private readonly CombatantViewController _combatantViewController;

    private readonly StatController _statController;
    private readonly MoveService _moveService;
    private readonly BattleTurnService _turnService;
    private readonly BattleResolutionService _resolutionService;
    private readonly IMoveProvider _playerProvider;
    private readonly IMoveProvider _enemyProvider;

    public BattleController(
        MoveController moveController,
        CombatantViewController combatantViewController,
        StatController statController,
        MoveService moveService,
        BattleTurnService turnService,
        BattleResolutionService resolutionService)
    {
        _moveController = moveController;
        _combatantViewController = combatantViewController;
        _statController = statController;
        _moveService = moveService;
        _turnService = turnService;
        _resolutionService = resolutionService;
        _playerProvider = new PlayerMoveProvider();
        _enemyProvider = new AIBattleMoveSelector();
    }

    public void Initialize(Hero hero, Character enemyCharacter)
    {
        var (player, enemy) = ConfigureCombatants(hero, enemyCharacter);
        _moveController.Initialize(player.Moves, _playerProvider.OnMoveSelected);
        _statController.Show(player);
        _combatantViewController.Create(player);
        _combatantViewController.Create(enemy);
        var battleData = new BattleContext(new List<Combatant> { player, enemy });

        _battleService = new BattleService(
            battleData,
            _turnService,
            _resolutionService,
            _moveService);
    }


    public async Awaitable<Combatant> Run()
    {
        while (_battleService.Phase != BattlePhase.Finished)
        {
            if (_battleService.Phase == BattlePhase.NeedMoveSelection)
            {
                var actor = _battleService.CurrentActor;
                var target = _battleService.CurrentTarget;

                _battleService.RemoveExpiredModifiers(actor);
                _statController.RefreshStatView(actor, target);

                // TODO: There could be one MoveProvider class with GetMove(Actor actor) method. This class reads actor.MoveProvider string and chooses the right provicer
                var provider = actor.Role == CombatantRole.Player
                    ? _playerProvider
                    : _enemyProvider;
                var move = await provider.GetMove(actor);

                _battleService.SubmitMove(move);

                _battleService.TickModifiers(actor);

                _combatantViewController.OnMoveExecuted(actor, target, move);
                _statController.RefreshStatView(actor, target);
            }

            if (_battleService.Phase == BattlePhase.ResolvingTurn)
            {
                _battleService.Advance();
            }
        }

        return _battleService.Winner;
    }

    public (Combatant, Combatant) ConfigureCombatants(
        Hero playerCharacter,
        Character enemyCharacter)
    {
        var player = playerCharacter.ToCombatant();
        var enemy = enemyCharacter.ToCombatant();

        player.Role = CombatantRole.Player;
        enemy.Role = CombatantRole.Enemy;

        return (player, enemy);
    }

    public void RemoveCombatants()
    {
        _combatantViewController.RemoveAll();
    }
}
