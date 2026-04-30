using UnityEngine;

public class MoveManagementState : IState
{
    private readonly RunFlowStateMachine _flow;
    private readonly RunSceneContext _context;

    public MoveManagementState(
        RunFlowStateMachine flow,
        RunSceneContext context)
    {
        _flow = flow;
        _context = context;
    }

    public async Awaitable Enter()
    {
        _context.MoveManagementBootstrapper.Show();

        // await _context.MoveManagementScreen.WaitForClose();

        // await _flow.EnterMap();
    }

    public Awaitable Exit()
    {
        _context.MoveManagementBootstrapper.Hide();
        return default;
    }
}