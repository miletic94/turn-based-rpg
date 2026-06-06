using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class MoveManagementState : IState
{
    private readonly GameplayStateMachine _stateMachine;
    private readonly GameplaySceneContext _context;

    private Dictionary<int, Move> _movesById;

    private MoveLoadoutService _moveLoadoutService;

    public MoveManagementState(
        GameplayStateMachine stateMachine,
        GameplaySceneContext context)
    {
        _stateMachine = stateMachine;
        _context = context;
    }

    public async void Enter()
    {
        var hero = _context.GameplayContext.Hero;

        // TODO: What to do if player already has a move that suddenly is reward offering
        var allMoves =
            hero.AvailableMoves
                .Concat(hero.EquippedMoves)
                .ToList();

        _movesById =
            allMoves.ToDictionary(x => x.Id);

        var loadout = new MoveLoadout
        {
            AvailableMoves =
                hero.AvailableMoves
                    .Select(x => x.Id)
                    .ToHashSet(),

            EquippedMoves =
                hero.EquippedMoves
                    .Select(x => x.Id)
                    .ToHashSet()
        };

        _moveLoadoutService =
            new MoveLoadoutService(loadout);

        var presenter =
            new MoveManagementPresenter();

        var presentation =
            await presenter.Build(
                hero.AvailableMoves,
                hero.EquippedMoves,
                loadout.MaxEquipped);

        _context.MoveManagementBootstrapper.Load(
            _moveLoadoutService,
            new MoveDescriptionService(_movesById),
            presentation,
            _context.UIFeedbackBus,
            HandleSaveRequested);
    }

    private void HandleSaveRequested()
    {
        var availableMoves =
            _moveLoadoutService.AvailableMoves
                .Select(id => _movesById[id])
                .ToList();

        var equippedMoves =
            _moveLoadoutService.EquippedMoves
                .Select(id => _movesById[id])
                .ToList();

        var hero =
            _context.GameplayContext.Hero;

        hero.SetAvailableMoves(
            availableMoves);

        hero.SetEquippedMoves(
            equippedMoves);

        _stateMachine.EnterMap();
    }

    public void Exit()
    {
        _context.MoveManagementBootstrapper.Unload();
    }
}