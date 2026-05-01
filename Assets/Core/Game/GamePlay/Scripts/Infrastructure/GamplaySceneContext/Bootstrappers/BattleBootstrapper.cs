using System.Collections.Generic;
using UnityEngine;

public class BattleBootstrapper : MonoBehaviour
{
    [SerializeField] BattleScreen BattleScreen;
    [SerializeField] private CombatantViewFactory _combatantViewFactory;
    [SerializeField] private MoveView _moveView;
    [SerializeField] private StatView _statView;
    CombatantViewBinder _combatantViewBinder;
    StatViewBinder _statViewBinder;
    MoveViewBinder _moveViewBinder;

    public async Awaitable<Character> InitializeAndRun(Character player, Character enemy)
    {
        _combatantViewBinder = new CombatantViewBinder(_combatantViewFactory, AppContext.EventBus);
        _statViewBinder = new StatViewBinder(AppContext.EventBus, _statView);
        _moveViewBinder = new MoveViewBinder(AppContext.EventBus, _moveView);
        ConfigureCombatants(player, enemy);

        SetupPresentation(player, enemy);

        var battleState = new BattleData(new List<Character> { player, enemy });

        var battleService = BuildBattleService(battleState);

        BattleScreen.Show();

        return await battleService.RunBattle();
    }

    public void Unload()
    {
        _combatantViewBinder.Unbind(AppContext.EventBus);
        _statViewBinder.Unbind();
        _moveViewBinder.Unbind();

        BattleScreen.Hide();
    }

    private void SetupPresentation(
        Character player,
        Character enemy)
    {
        _combatantViewBinder.Register(player);
        _combatantViewBinder.Register(enemy);
        _combatantViewBinder.Bind();

        _moveViewBinder.Bind();

        _statViewBinder.Show(player);
        _statViewBinder.Bind();
    }

    private BattleService BuildBattleService(
        BattleData battleState)
    {
        // Core execution
        var effectExecutionService =
            new MoveEffectExecutionService();

        var moveService =
            new MoveService(effectExecutionService, AppContext.EventBus);

        // Battle orchestration
        var turnService = new BattleTurnService();

        var resolutionService = new BattleResolutionService();

        // Main battle runner
        return new BattleService(
            battleState,
            turnService,
            resolutionService,
            moveService);
    }

    private void ConfigureCombatants(Character player, Character enemy)
    {
        player.Role = CombatantRole.Player;
        enemy.Role = CombatantRole.Enemy;

        player.MoveSelector =
            new PlayerBattleMoveSelector(AppContext.EventBus);

        enemy.MoveSelector =
            new AIBattleMoveSelector();
    }
}