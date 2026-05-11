using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagementBootstrapper : MonoBehaviour
{
    [SerializeField] StatsManagementScreen _statsManagementScreen;
    [SerializeField] StatsManagementView _statsManagementView;
    private StatsManagementService _statsManagementService;
    private StatsManagementController _statsManagementController;



    public void Load(GameplayContext context, Action onSave)
    {
        // TODO: This can be done better
        if (_statsManagementService == null)
            _statsManagementService = new StatsManagementService(context);
        if (_statsManagementController == null)
            _statsManagementController = new StatsManagementController(_statsManagementView, _statsManagementService, onSave);

        _statsManagementController.Initialize();
        _statsManagementScreen.Show();
    }
    public void Unload()
    {
        _statsManagementScreen.Hide();
    }
}