using UnityEngine;

public class MapState : IState
{
    private readonly RunFlowStateMachine _flow;
    private readonly RunSceneContext _context;

    public MapState(
        RunFlowStateMachine flow,
        RunSceneContext context)
    {
        _flow = flow;
        _context = context;
    }

    public async Awaitable Enter()
    {
        _context.MapBootstrapper.Show();

        // var result =
        //     await _context.MapBootstrapper.WaitForSelection();

        // if (result.OpenMoveManagement)
        // {
        //     await _flow.EnterMoveManagement();
        //     return;
        // }

        // await _flow.EnterBattle(result.SelectedEncounter);
    }

    public Awaitable Exit()
    {
        _context.MapBootstrapper.Hide();
        return default;
    }
}