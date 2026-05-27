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
        _context.MoveManagementBootstrapper.Load(
            _context.GameplayContext.Hero,
            OnSave
        );
    }

    public void OnSave(List<Move> availableMoves, List<Move> equippedMoves)
    {
        _context.GameplayContext.Hero.SetAvailableMoves(availableMoves);
        _context.GameplayContext.Hero.SetEquippedMoves(equippedMoves);
        _gameplayStateMachine.EnterMap();
    }

    public void Exit()
    {
        // _context.MoveManagementBootstrapper.Unload();
    }
}