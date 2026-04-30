using UnityEngine;

public class RewardState : IState
{
    private readonly RunFlowStateMachine _flow;
    private readonly RunSceneContext _context;
    private readonly Move _move;

    public RewardState(
        RunFlowStateMachine flow,
        RunSceneContext context)
    {
        _flow = flow;
        _context = context;
    }

    public async Awaitable Enter()
    {
        _context.RewardBootstrapper.Show();

        // await _context.RewardScreen.WaitForContinue();

        // await _flow.EnterMap();
    }

    public Awaitable Exit()
    {
        _context.RewardBootstrapper.Hide();
        return default;
    }
}