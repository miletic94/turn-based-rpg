using UnityEngine;

public class StatsManagementBootstrapper : MonoBehaviour
{
    [SerializeField] StatsManagementScreen _statsManagementScreen;
    [SerializeField] StatsManagementView _statsManagementView;

    public void Load(Hero hero)
    {
        var statsManagementController = new StatsManagementController(_statsManagementView);
        statsManagementController.Initialize(new StatsViewData(hero));
        _statsManagementScreen.Show();
    }
    public void Unload()
    {
        _statsManagementScreen.Hide();
    }
}