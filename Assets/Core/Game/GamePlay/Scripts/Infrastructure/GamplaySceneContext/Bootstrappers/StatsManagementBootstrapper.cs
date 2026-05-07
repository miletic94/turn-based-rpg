using UnityEngine;

public class StatsManagementBootstrapper : MonoBehaviour
{
    [SerializeField] StatsManagementScreen _statsManagementScreen;
    [SerializeField] StatsManagementView _statsManagementView;

    public void Load(Hero hero)
    {
        var statsManagementService = new StatsManagementService(new StatsViewData(hero));
        var statsManagementController = new StatsManagementController(_statsManagementView, statsManagementService);
        statsManagementController.Initialize();
        _statsManagementScreen.Show();
    }
    public void Unload()
    {
        _statsManagementScreen.Hide();
    }
}