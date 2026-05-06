using System;
using System.Collections.Generic;

public class MoveManagementState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;


    public MoveManagementState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public void Enter()
    {
        var loadout = new MoveLoadout
        {
            AvailableMoves = new List<Move>(_context.GameplayContext.Hero.AvailableMoves),
            EquippedMoves = new List<Move>(_context.GameplayContext.Hero.EquippedMoves),
            MaxEquipped = 4
        };

        foreach (var equipped in loadout.EquippedMoves)
        {
            loadout.AvailableMoves.Remove(equipped);
        }

        var service = new MoveLoadoutService(loadout);

        _context.MoveManagementBootstrapper.Initialize(
            loadout,
            service,
            OnSave
        );
    }

    public void OnSave()
    {
        _gameplayStateMachine.EnterMap();
    }

    public void Exit()
    {
        _context.MoveManagementBootstrapper.Unload();
    }
}