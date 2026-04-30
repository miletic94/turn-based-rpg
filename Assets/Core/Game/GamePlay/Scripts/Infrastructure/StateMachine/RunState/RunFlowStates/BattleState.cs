using UnityEngine;

public class BattleState : IState
{
    private readonly RunFlowStateMachine _flow;
    private readonly RunSceneContext _context;

    public BattleState(
        RunFlowStateMachine flow,
        RunSceneContext context)
    {
        _flow = flow;
        _context = context; ;
    }

    public async Awaitable Enter()
    {

        var result = await _context.BattleBootstrapper.InitializeAndRun();

        if (result.Role == CombatantRole.Player)
            await _flow.EnterReward(); // await _flow.EnterReward(result.LearnedMove)
        else
            await _flow.EnterMap();
    }

    public Awaitable Exit()
    {
        _context.BattleBootstrapper.Unload();
        return default;
    }
}