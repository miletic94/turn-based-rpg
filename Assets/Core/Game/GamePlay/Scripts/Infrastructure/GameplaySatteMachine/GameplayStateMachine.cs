using UnityEngine;

public class GameplayStateMachine
{
    private readonly StateMachine _machine = new();
    private readonly GameplaySceneContext _context;

    public GameplayStateMachine(GameplaySceneContext context)
    {
        _context = context;
    }

    public void EnterMap()
    {
        _machine.SwitchState(
        new MapState(this, _context));
    }

    public void EnterBattle(Character player, Character enemy)
    {
        _machine.SwitchState(
            new BattleState(this, _context, player, enemy));
    }

    public void EnterReward()
        => _machine.SwitchState(
            new RewardState(this, _context));

    public void EnterMoveManagement()
        => _machine.SwitchState(
            new MoveManagementState(this, _context));
}