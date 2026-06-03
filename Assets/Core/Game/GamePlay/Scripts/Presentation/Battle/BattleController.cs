using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BattleController
{
    private BattleService _battleService;
    private BattleMovePanelController _battleMoveSelectionController;
    private readonly CombatantViewController _combatantViewController;

    private readonly CharacterInfoPanelsController _characteRInfoPanelsController;
    private readonly MoveService _moveService;
    private readonly BattleTurnService _turnService;
    private readonly BattleResolutionService _resolutionService;
    private readonly IMoveProvider _playerProvider;
    private readonly IMoveProvider _enemyProvider;

    public BattleController(
        BattleMovePanelController battleMoveController,
        CombatantViewController combatantViewController,
        CharacterInfoPanelsController characterInfoPanelsController,
        MoveService moveService,
        BattleTurnService turnService,
        BattleResolutionService resolutionService)
    {
        _battleMoveSelectionController = battleMoveController;
        _combatantViewController = combatantViewController;
        _characteRInfoPanelsController = characterInfoPanelsController;
        _moveService = moveService;
        _turnService = turnService;
        _resolutionService = resolutionService;
        _playerProvider = new PlayerMoveProvider();
        _enemyProvider = new AIBattleMoveSelector();
    }

    public async Awaitable Initialize(Hero hero, Character enemyCharacter)
    {
        var (player, enemy) = await ConfigureCombatants(hero, enemyCharacter);

        _ = _battleMoveSelectionController.Initialize(player.Moves, _playerProvider.OnMoveSelected);

        _characteRInfoPanelsController.CreatePanels(player, enemy);
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
                _battleService.TickModifiers(actor);

                _characteRInfoPanelsController.RefreshStatsPanels(actor, target);

                // TODO: There could be one MoveProvider class with GetMove(Actor actor) method. This class reads actor.MoveProvider string and chooses the right provicer
                var provider = actor.Role == CombatantRole.Player
                    ? _playerProvider
                    : _enemyProvider;
                var move = await provider.GetMove(actor);

                _battleService.SubmitMove(move);

                Debug.Log($@"MOVE
                actor: {actor}
                target: {target}");
                _characteRInfoPanelsController.SetHealthBars(actor, target);
            }

            if (_battleService.Phase == BattlePhase.ResolvingTurn)
            {
                _battleService.Advance();
            }
        }

        return _battleService.Winner;
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
