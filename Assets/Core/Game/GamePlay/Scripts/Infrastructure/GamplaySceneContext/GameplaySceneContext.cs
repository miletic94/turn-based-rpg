using UnityEngine;

public class GameplaySceneContext : MonoBehaviour
{
    [Header("Feature Bootstrappers")]
    public MapBootstrapper MapBootstrapper;
    public BattleBootstrapper BattleBootstrapper;
    public RewardBootstrapper RewardBootstrapper;
    public MoveManagementBootstrapper MoveManagementBootstrapper;

    [Header("Run State")]
    public RunSession RunSession { get; private set; }

    public void InitializeRun(Hero hero)
    {
        RunSession = new RunSession(hero);
    }
}