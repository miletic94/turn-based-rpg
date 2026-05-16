using UnityEngine;

public class BattleBootstrapper : MonoBehaviour
{
    [SerializeField] BattleScreen _battleScreen;
    [SerializeField] private CombatantViewFactory _combatantViewFactory;
    [SerializeField] private BattleMoveView _battleMoveView;
    [SerializeField] private StatView _statView;
    [SerializeField] private TooltipView _tooltipView;
    private BattleController _battleController;

    public BattleController Load()
    {
        _battleScreen.Show();

        var effectExecutionService = new MoveEffectService();

        var combatantViewController = new CombatantViewController(_combatantViewFactory);
        var statController = new StatController(_statView);

        var moveService = new MoveService(effectExecutionService);
        var turnService = new BattleTurnService();
        var resolutionService = new BattleResolutionService();


        _battleController = new BattleController(
            new BattleMoveController(
                _battleMoveView,
                new MoveTooltipBinder(
                    new MoveDescriptionService()
                    , _tooltipView)),
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