public class RewardState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    public RewardState(
        GameplayStateMachine flow,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = flow;
        _context = context;
    }

    public void Enter()
    {
        _context.RewardBootstrapper.Load(_context.GameplayContext.CurrentEnemy, HandleRewardSelected); ;
    }
    public void HandleRewardSelected(Move move)
    {
        _context.GameplayContext.Hero.AvailableMoves.Add(move);
        _gameplayStateMachine.EnterXp();
    }
    public void Exit()
    {
        _context.RewardBootstrapper.Unload();
    }
}