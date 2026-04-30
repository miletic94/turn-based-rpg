// ======================================================
// BATTLE CONTROLLER
// Composition root for battle feature.
// Sets up characters, selectors, services, UI wiring.
// Then hands execution off to BattleService.
// ======================================================

using System.Collections.Generic;
using UnityEngine;

public class BattleController
{
    private readonly CombatantViewBinder _combatantViewBinder;
    private readonly MoveController _moveController;
    private readonly StatController _statController;
    private readonly EventBus _eventBus;

    public BattleController(
        EventBus eventBus,
        CombatantViewBinder combatantViewBinder,
        MoveController moveController,
        StatController statController)
    {
        _eventBus = eventBus;
        _combatantViewBinder = combatantViewBinder;
        _moveController = moveController;
        _statController = statController;
    }

    public async Awaitable Run()
    {
        // ==================================================
        // 1. LOAD DOMAIN DATA
        // ==================================================
        var characters = LoadCharacters();

        var player = characters[0];
        var enemy = characters[1];

        // ==================================================
        // 2. CONFIGURE BATTLE PARTICIPANTS
        // ==================================================
        ConfigureCombatants(player, enemy);

        // ==================================================
        // 3. CREATE VISUALS / UI
        // ==================================================
        SetupPresentation(player, enemy);

        // ==================================================
        // 4. BUILD APPLICATION LAYER
        // ==================================================
        var battleState = new BattleState(
            new List<Character> { player, enemy });

        var battleService = BuildBattleService(battleState);

        // ==================================================
        // 5. RUN
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
            new PlayerCombatMoveSelector(_moveController);

        enemy.MoveSelector =
            new AICombatMoveSelector();
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
        _combatantViewBinder.Bind(_eventBus);

        _moveController.ShowMoves(player.Moves);

        _statController.Show(player);
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