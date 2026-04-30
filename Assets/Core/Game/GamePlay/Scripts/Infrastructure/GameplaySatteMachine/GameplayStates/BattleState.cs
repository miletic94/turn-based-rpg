using UnityEngine;

public class BattleState : IState
{
    private readonly GameplayStateMachine _flow;
    private readonly GameplaySceneContext _context;

    public BattleState(
        GameplayStateMachine flow,
        GameplaySceneContext context)
    {
        _flow = flow;
        _context = context; ;
    }

    public async void Enter()
    {

        var result = await _context.BattleBootstrapper.InitializeAndRun();

        if (result.Role == CombatantRole.Player)
            _flow.EnterReward(); // await _flow.EnterReward(result.LearnedMove)
        else
            _flow.EnterMap();
    }

    public void Exit()
    {
        _context.BattleBootstrapper.Unload();
    }
}