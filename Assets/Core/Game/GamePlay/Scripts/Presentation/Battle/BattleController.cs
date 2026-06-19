using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BattleController
{
    private BattleService _battleService;
    private BattleMovePanelController _movePanelController;
    private readonly CombatantViewController _combatantViewController;

    private readonly CharacterInfoPanelsController _characterInfoPanelsController;
    private readonly MoveEffectCalculationService _moveEffectCalculationService;
    private readonly MoveExecutionService _moveExecutionService;
    private readonly BattleResolutionService _resolutionService;
    private MoveSelectionService _moveSelectionService;


    public BattleController(
        BattleMovePanelController movePanelController,
        CombatantViewController combatantViewController,
        CharacterInfoPanelsController characterInfoPanelsController,
        MoveEffectCalculationService moveEffectCalculationService,
        MoveExecutionService moveExecutionService,
        BattleResolutionService resolutionService)
    {
        _movePanelController = movePanelController;
        _combatantViewController = combatantViewController;
        _characterInfoPanelsController = characterInfoPanelsController;
        _moveEffectCalculationService = moveEffectCalculationService;
        _moveExecutionService = moveExecutionService;
        _resolutionService = resolutionService;
        _moveSelectionService = new MoveSelectionService();
    }

    public async Awaitable Initialize(Hero hero, Character enemyCharacter)
    {
        var (player, enemy) = await ConfigureCombatants(hero, enemyCharacter);

        _ = _movePanelController.Initialize(player.Moves, _moveSelectionService.SelectMove);

        _characterInfoPanelsController.CreatePanels(player, enemy);
        _combatantViewController.Create(player);
        _combatantViewController.Create(enemy);
        var battleContext = new BattleContext(new List<Combatant> { player, enemy });

        _battleService = new BattleService(
            battleContext,
            _resolutionService,
            _moveEffectCalculationService,
            _moveExecutionService,
            new PlayerMoveProvider(_moveSelectionService),
            new AIMoveProvider(_moveEffectCalculationService));
    }


    public async Awaitable<BattleFinishedUpdate> Run()
    {
        while (true)
        {
            var update =
                await _battleService.Step();

            switch (update)
            {
                case TurnStartedUpdate turn:
                    HandleTurnStarted(turn);
                    break;

                case MoveExecutedUpdate moveExecutedUpdate:
                    await HandleMoveExecuted(moveExecutedUpdate);
                    break;

                case BattleFinishedUpdate finishedResult:
                    return finishedResult;
            }
        }
    }

    private void HandleTurnStarted(
    TurnStartedUpdate update)
    {
        _characterInfoPanelsController
            .RefreshStatsPanels(
                update.Actor,
                update.Target);
    }

    private async Awaitable HandleMoveExecuted(
    MoveExecutedUpdate update)
    {
        _characterInfoPanelsController.SetHealthBars(update.Actor, update.Target);
        await _combatantViewController.ShowMoveEffect(update.MoveEffect);
    }

    public async Awaitable<(Combatant, Combatant)> ConfigureCombatants(
        Hero playerCharacter,
        Character enemyCharacter)
    {
        var playerSpriteHandle = Addressables.LoadAssetAsync<Sprite>(playerCharacter.SpriteAddress);
        var enemySpriteHandle = Addressables.LoadAssetAsync<Sprite>(enemyCharacter.SpriteAddress);
        var sprites = await Task.WhenAll(playerSpriteHandle.Task, enemySpriteHandle.Task);
        var playerSprite = sprites[0];
        var enemySprite = sprites[1];

        var player = new Combatant(
            playerCharacter.Name,
            playerCharacter.Health,
            new CombatantStats(playerCharacter.Attack, playerCharacter.Defense, playerCharacter.Magic),
            playerSprite,
            playerCharacter.EquippedMoves);

        var enemy = new Combatant(
            enemyCharacter.Name,
            enemyCharacter.Health,
            new CombatantStats(enemyCharacter.Attack, enemyCharacter.Defense, enemyCharacter.Magic),
            enemySprite,
            enemyCharacter.Moves);

        player.Role = CombatantRole.Player;
        enemy.Role = CombatantRole.Enemy;

        return (player, enemy);
    }

    public void RemoveCombatants()
    {
        _combatantViewController.RemoveAll();
    }
}
