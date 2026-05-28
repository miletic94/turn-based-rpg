using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneContext : MonoBehaviour
{
    [Header("Feature Bootstrappers")]
    public MapBootstrapper MapBootstrapper;
    public BattleBootstrapper BattleBootstrapper;
    public RewardBootstrapper RewardBootstrapper;
    public MoveManagementBootstrapper MoveManagementBootstrapper;
    public StatsManagementBootstrapper StatsManagementBootstrapper;
    public XpBootstrapper XpBootstrapper;
    public TooltipRouter TooltipRouter;

    [Header("Run State")]
    public GameplayContext GameplayContext { get; private set; }
    public UIFeedbackBus UIFeedbackBus { get; private set; }

    public void Initialize(Hero hero, List<Character> enemies)
    {
        GameplayContext = new GameplayContext(hero, enemies);
        UIFeedbackBus = new UIFeedbackBus();
        TooltipRouter.Initialize(UIFeedbackBus);
    }
}