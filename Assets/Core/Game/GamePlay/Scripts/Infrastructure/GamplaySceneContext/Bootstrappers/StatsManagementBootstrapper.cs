using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagementBootstrapper : MonoBehaviour
{
    [SerializeField] StatsManagementScreen _statsManagementScreen;
    [SerializeField] StatsManagementView _statsManagementView;

    public void Load(Hero hero, Action<IEnumerable<StatData>, int> onSave)
    {
        var statsManagementService = new StatsManagementService(new StatsViewData(hero));
        var statsManagementController = new StatsManagementController(_statsManagementView, statsManagementService, onSave);
        statsManagementController.Initialize();
        _statsManagementScreen.Show();
    }
    public void Unload()
    {
        _statsManagementScreen.Hide();
    }
}