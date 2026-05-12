using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagementController
{
    private readonly StatsManagementView _view;
    private readonly StatsManagementService _service;
    private readonly Action _onSave;

    public StatsManagementController(
        StatsManagementView view,
        StatsManagementService service,
        Action onSave
        )
    {
        _view = view;
        _service = service;
        _onSave = onSave;
    }

    public void Initialize()
    {
        _view.Initialize(
            _service.GetStats(),
            _service.GetAvailablePoints(),
            OnPlusClicked,
            OnMinusClicked,
            OnSaveButtonClicked);
    }

    private void OnPlusClicked(StatType type)
    {
        _service.AddStat(type);
        _service.SetAvailablePoints(_service.GetAvailablePoints() - 1);

        Refresh();
    }

    private void OnMinusClicked(StatType type)
    {
        _service.SubstractStat(type);
        _service.SetAvailablePoints(_service.GetAvailablePoints() + 1);

        Refresh();
    }
    private void OnSaveButtonClicked()
    {
        // TOOD: Save to the database
        _onSave?.Invoke();
    }

    private void Refresh()
    {
        _view.Refresh(
            _service.GetStats(),
            _service.GetAvailablePoints());
    }
}