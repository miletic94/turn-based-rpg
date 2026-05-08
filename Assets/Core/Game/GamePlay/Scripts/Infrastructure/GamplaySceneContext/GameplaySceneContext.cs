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

    [Header("Run State")]
    public GameplayContext GameplayContext { get; private set; }

    public void InitializeRun(Hero hero, List<Character> enemies)
    {
        GameplayContext = new GameplayContext(hero, enemies);
    }
}