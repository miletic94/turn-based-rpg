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
    private readonly MoveService _moveService;
    private readonly BattleTurnService _turnService;
    private readonly BattleResolutionService _resolutionService;
    private MoveSelectionService _moveSelectionService;


    public BattleController(
        BattleMovePanelController movePanelController,
        CombatantViewController combatantViewController,
        CharacterInfoPanelsController characterInfoPanelsController,
        MoveService moveService,
        BattleTurnService turnService,
        BattleResolutionService resolutionService)
    {
        _movePanelController = movePanelController;
        _combatantViewController = combatantViewController;
        _characterInfoPanelsController = characterInfoPanelsController;
        _moveService = moveService;
        _turnService = turnService;
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
            _turnService,
            _resolutionService,
            _moveService,
            new PlayerMoveProvider(_moveSelectionService),
            new AIBattleMoveSelector());
    }


    public async Awaitable<Combatant> Run()
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

                case BattleFinishedUpdate finished:
                    return finished.Winner;
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
        await _combatantViewController.ShowMoveResult(update.MoveResult);
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
