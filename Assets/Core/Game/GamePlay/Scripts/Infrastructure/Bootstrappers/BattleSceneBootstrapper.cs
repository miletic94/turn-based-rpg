using System.Collections.Generic;
using UnityEngine;

public class BattleSceneBootstrapper : MonoBehaviour
{
    [SerializeField] private CombatantViewFactory _combatantViewFactory;
    [SerializeField] private MoveView _moveView;
    [SerializeField] private StatView _statView;
    CombatantViewBinder _combatantViewBinder;
    StatViewBinder _statViewBinder;
    MoveViewBinder _moveViewBinder;
    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = new EventBus();
        _combatantViewBinder = new CombatantViewBinder(_combatantViewFactory, _eventBus);
        _statViewBinder = new StatViewBinder(_eventBus, _statView);
        _moveViewBinder = new MoveViewBinder(_eventBus, _moveView);
    }

    private async void Start()
    {
        await InitializeAndRun();
    }

    private async Awaitable InitializeAndRun()
    {
        // ==================================================
        // 1. EVENT SYSTEM
        // ==================================================
        _eventBus = new EventBus();

        // ==================================================
        // 2. BINDERS
        // ==================================================
        _combatantViewBinder = new CombatantViewBinder(_combatantViewFactory, _eventBus);
        _statViewBinder = new StatViewBinder(_eventBus, _statView);
        _moveViewBinder = new MoveViewBinder(_eventBus, _moveView);

        // ==================================================
        // 3. LOAD DOMAIN
        // ==================================================
        var characters = LoadCharacters();

        var player = characters[0];
        var enemy = characters[1];

        // ==================================================
        // 4. CONFIGURE DOMAIN
        // ==================================================
        ConfigureCombatants(player, enemy);

        // ==================================================
        // 5. PRESENTATION SETUP
        // ==================================================
        SetupPresentation(player, enemy);

        // ==================================================
        // 6. BATTLE STATE
        // ==================================================
        var battleState = new BattleState(
            new List<Character> { player, enemy });

        // ==================================================
        // 7. SERVICE LAYER
        // ==================================================
        var battleService = BuildBattleService(battleState);

        // ==================================================
        // 8. RUN GAME
        // ==================================================
        await battleService.RunBattle();
    }

    // ======================================================
    // DOMAIN LOADING
    // ======================================================

    private List<Character> LoadCharacters()
    {
        return new CharacterDeserializer().Deserialize();
    }

    // ======================================================
    // COMBATANT SETUP
    // ======================================================

    private void ConfigureCombatants(
        Character player,
        Character enemy)
    {
        player.Role = CombatantRole.Player;
        enemy.Role = CombatantRole.Enemy;

        player.MoveSelector =
            new PlayerBattleMoveSelector(_eventBus);

        enemy.MoveSelector =
            new AIBattleMoveSelector();
    }

    // ======================================================
    // PRESENTATION SETUP
    // ======================================================

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

    // ======================================================
    // APPLICATION COMPOSITION
    // ======================================================

    private BattleService BuildBattleService(
        BattleState battleState)
    {
        // Core execution
        var effectExecutionService =
            new MoveEffectExecutionService();

        var moveService =
            new MoveService(effectExecutionService, _eventBus);

        // Battle orchestration
        var turnService =
            new BattleTurnService();

        var resolutionService =
            new BattleResolutionService();

        // Main battle runner
        return new BattleService(
            battleState,
            turnService,
            resolutionService,
            moveService);
    }
}