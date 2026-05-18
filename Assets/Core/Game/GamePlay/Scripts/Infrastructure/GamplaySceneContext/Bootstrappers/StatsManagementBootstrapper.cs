using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagementBootstrapper : MonoBehaviour
{
    [SerializeField] StatsManagementScreen _statsManagementScreen;
    [SerializeField] StatsManagementPanelView _statsManagementView;
    private StatsManagementService _statsManagementService;
    private StatsManagementPanelController _statsManagementPanelController;



    public void Load(GameplayContext context, Action onSave)
    {
        // TODO: This can be done better
        if (_statsManagementService == null)
            _statsManagementService = new StatsManagementService(context);
        if (_statsManagementPanelController == null)
            _statsManagementPanelController = new StatsManagementPanelController(
                _statsManagementView,
                _statsManagementService);

        _statsManagementPanelController.Initialize(onSave);
        _statsManagementScreen.Show();
    }
    public void Unload()
    {
        _statsManagementScreen.Hide();
    }
}