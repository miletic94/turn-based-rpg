using UnityEngine;

public class RewardState : IState
{
    private readonly GameplayStateMachine _flow;
    private readonly GameplaySceneContext _context;
    private readonly Move _move;

    public RewardState(
        GameplayStateMachine flow,
        GameplaySceneContext context)
    {
        _flow = flow;
        _context = context;
    }

    public void Enter()
    {
        _context.RewardBootstrapper.Load();

        // await _context.RewardScreen.WaitForContinue();

        // await _flow.EnterMap();
    }

    public void Exit()
    {
        _context.RewardBootstrapper.Unload();
    }
}