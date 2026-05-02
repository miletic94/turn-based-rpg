using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BattleBootstrapper : MonoBehaviour
{
    [SerializeField] BattleScreen BattleScreen;
    [SerializeField] private CombatantViewFactory _combatantViewFactory;
    [SerializeField] private MoveView _moveView;
    [SerializeField] private StatView _statView;
    CombatantViewBinder _combatantViewBinder;
    StatViewBinder _statViewBinder;
    MoveViewBinder _moveViewBinder;
    // TODO: This all sucks, need to refactor how the battle scene is initialized and how the state machine interacts with it. This is just a quick and dirty way to get something working for now.
    public (Combatant, Combatant ombatant) Initialize(Character playerCharacter, Character enemyCharacter)
    {
        _combatantViewBinder = new CombatantViewBinder(_combatantViewFactory, AppContext.EventBus);
        _statViewBinder = new StatViewBinder(AppContext.EventBus, _statView);
        _moveViewBinder = new MoveViewBinder(AppContext.EventBus, _moveView);

        var (player, enemy) = ConfigureCombatants(playerCharacter, enemyCharacter);

        SetupPresentation(player, enemy);

        BattleScreen.Show();
        return (player, enemy);
    }

    public async Awaitable<Combatant> Run(Combatant player, Combatant enemy)
    {
        var battleState = new BattleData(new List<Combatant> { player, enemy });

        var battleService = BuildBattleService(battleState);

        return await battleService.RunBattle();
    }

    public void Unload()
    {
        _combatantViewBinder.Unbind(AppContext.EventBus);
        _combatantViewBinder.UnregisterAll();
        _statViewBinder.Unbind();
        _moveViewBinder.Unbind();

        BattleScreen.Hide();
    }

    private void SetupPresentation(
        Combatant player,
        Combatant enemy)
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

    private (Combatant, Combatant) ConfigureCombatants(Character playerCharacter, Character enemyCharacter)
    {

        var player = new Combatant(
            playerCharacter.Name,
            playerCharacter.Health,
            playerCharacter.Attack,
            playerCharacter.Defense,
            playerCharacter.Magic,
            4,
            playerCharacter.Moves);

        var enemy = new Combatant(
            enemyCharacter.Name,
            enemyCharacter.Health,
            enemyCharacter.Attack,
            enemyCharacter.Defense,
            enemyCharacter.Magic,
            4,
            enemyCharacter.Moves);

        player.Role = CombatantRole.Player;
        enemy.Role = CombatantRole.Enemy;

        player.MoveSelector =
            new PlayerBattleMoveSelector(AppContext.EventBus);

        enemy.MoveSelector =
            new AIBattleMoveSelector();
        return (player, enemy);
    }
}