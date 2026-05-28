using UnityEngine;

public class BattleBootstrapper : MonoBehaviour
{
    [SerializeField] Screen _battleScreen;
    [SerializeField] private CombatantViewFactory _combatantViewFactory;
    [SerializeField] private BattleMoveListView _moveListView;
    [SerializeField] private BattleStatPanelView _battleStatPanelView;
    private BattleController _battleController;

    public BattleController Load()
    {
        _battleScreen.Show();

        var battleMoveController =
            new BattleMovePanelController(_moveListView);

        var effectExecutionService = new MoveEffectService();

        var combatantViewController = new CombatantViewController(_combatantViewFactory);
        var battleStatPanelController = new BattleStatPanelController(_battleStatPanelView);

        var moveService = new MoveService(effectExecutionService);
        var turnService = new BattleTurnService();
        var resolutionService = new BattleResolutionService();


        _battleController = new BattleController(
            battleMoveController,
            combatantViewController,
            battleStatPanelController,
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