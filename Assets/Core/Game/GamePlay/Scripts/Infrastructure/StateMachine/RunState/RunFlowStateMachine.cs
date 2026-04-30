using UnityEngine;

public class RunFlowStateMachine
{
    private readonly StateMachine _machine = new();
    private readonly RunSceneContext _context;

    public RunFlowStateMachine(RunSceneContext context)
    {
        _context = context;
    }

    public Awaitable EnterMap()
        => _machine.SwitchState(
            new MapState(this, _context));

    public Awaitable EnterBattle()
        => _machine.SwitchState(
            new BattleState(this, _context));

    public Awaitable EnterReward()
        => _machine.SwitchState(
            new RewardState(this, _context));

    public Awaitable EnterMoveManagement()
        => _machine.SwitchState(
            new MoveManagementState(this, _context));
}