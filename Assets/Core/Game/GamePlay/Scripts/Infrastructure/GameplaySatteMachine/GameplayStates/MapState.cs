using UnityEngine;

public class MapState : IState
{
    private readonly GameplayStateMachine _flow;
    private readonly GameplaySceneContext _context;

    public MapState(
        GameplayStateMachine flow,
        GameplaySceneContext context)
    {
        _flow = flow;
        _context = context;
    }

    public void Enter()
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

    public void Exit()
    {
        _context.MapBootstrapper.Hide();
    }
}