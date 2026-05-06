using UnityEngine;

public class BattleBootstrapper : MonoBehaviour
{
    [SerializeField] BattleScreen _battleScreen;
    [SerializeField] private CombatantViewFactory _combatantViewFactory;
    [SerializeField] private MoveView _moveView;
    [SerializeField] private StatView _statView;
    private BattleController _battleController;

    public BattleController Load()
    {
        _battleScreen.Show();

        var effectExecutionService = new MoveEffectExecutionService();

        var moveController = new MoveController(_moveView);
        var combatantViewController = new CombatantViewController(_combatantViewFactory);
        var statController = new StatController(_statView);

        var moveService = new MoveService(effectExecutionService, AppContext.EventBus);
        var turnService = new BattleTurnService();
        var resolutionService = new BattleResolutionService();


        _battleController = new BattleController(
            moveController,
            combatantViewController,
            statController,
            moveService,
            turnService,
            resolutionService
        );
        return _battleController;
    }

    public void Unload()
    {
        _battleScreen.Hide();
        _battleController.RemoveCombatants();
    }
}