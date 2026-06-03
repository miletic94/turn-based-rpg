using System;
using Presentation.StatsManagement;
using UnityEngine;

public class StatsManagementBootstrapper : MonoBehaviour
{
    [SerializeField] Screen _statsManagementScreen;
    [SerializeField] Background _statsManagementBackground;
    [SerializeField] StatManagementView _statsManagementView;
    private StatsManagementService _statsManagementService;
    private StatsManagementController _statsManagementController;



    public void Load(GameplayContext context, Action<StatSaveData> onSave)
    {
        // TODO: This can be done better
        if (_statsManagementService == null)
            _statsManagementService = new StatsManagementService();
        if (_statsManagementController == null)
            _statsManagementController = new StatsManagementController(
                _statsManagementView,
                _statsManagementService);

        _statsManagementController.CreateStatPanel(context.Hero, onSave);
        _statsManagementScreen.Show();
        _statsManagementBackground.Show();
    }
    public void Unload()
    {
        _statsManagementScreen.Hide();
        _statsManagementBackground.Hide();
    }
}