using System.Linq;

public class BattleState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    public BattleState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public async void Enter()
    {
        var hero = _context.GameplayContext.Hero;
        var battleController = _context.BattleBootstrapper.Load(
            _context.UIFeedbackBus,
            new MoveDescriptionService());
        await battleController.Initialize(hero, _context.GameplayContext.CurrentEnemy);

        var result = await battleController.Run();

        if (result.Winner.Role == CombatantRole.Player)
        {
            _context.GameplayContext.Hero.AddBeatenEnemy(result.Enemy.Name);
            _gameplayStateMachine.EnterReward();
        }
        else
            _gameplayStateMachine.EnterXp();
    }
    public void Exit()
    {
        _context.BattleBootstrapper.Unload();
    }
}