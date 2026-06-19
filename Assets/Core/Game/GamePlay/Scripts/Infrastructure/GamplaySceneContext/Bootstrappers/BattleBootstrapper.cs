using UnityEngine;

public class BattleBootstrapper : MonoBehaviour
{
    [SerializeField] Screen _battleScreen;
    [SerializeField] Background _batleBackground;
    [SerializeField] private CombatantViewFactory _combatantViewFactory;
    [SerializeField] private BattleMoveListView _moveListView;
    [SerializeField] private CharacterInfoPanelsView _characterInfoPanelsView;
    [SerializeField] private MoveTelegraphView _moveTelegraphView;

    private BattleController _battleController;
    private CharacterInfoPanelsController _characterInfoPanelsController;

    public BattleController Load(UIFeedbackBus uiFeedbackBus, MoveDescriptionService moveDescriptionService)
    {
        _battleScreen.Show();
        _batleBackground.Show();

        var battleMoveController =
            new BattleMovePanelController(_moveListView, uiFeedbackBus, moveDescriptionService);

        var combatantViewController = new CombatantViewController(_combatantViewFactory);
        _characterInfoPanelsController = new CharacterInfoPanelsController(_characterInfoPanelsView);
        var moveEffectCalculationService = new MoveEffectCalculationService();
        var moveExecutionService = new MoveExecutionService();
        var resolutionService = new BattleResolutionService();


        _battleController = new BattleController(
            battleMoveController,
            combatantViewController,
            _characterInfoPanelsController,
            moveEffectCalculationService,
            moveExecutionService,
            resolutionService
        );
        return _battleController;
    }

    public void Unload()
    {
        _battleScreen.Hide();
        _batleBackground.Hide();
        _battleController.RemoveCombatants();
    }
}