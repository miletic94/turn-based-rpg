using UnityEngine;

public class MoveManagementState : IState
{
    private readonly GameplayStateMachine _flow;
    private readonly GameplaySceneContext _context;

    public MoveManagementState(
        GameplayStateMachine flow,
        GameplaySceneContext context)
    {
        _flow = flow;
        _context = context;
    }

    public async void Enter()
    {
        _context.MoveManagementBootstrapper.Show();

        // await _context.MoveManagementScreen.WaitForClose();

        // await _flow.EnterMap();
    }

    public void Exit()
    {
        _context.MoveManagementBootstrapper.Hide();
    }
}