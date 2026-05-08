using System;
using System.Collections.Generic;

public class StatsManagementController
{
    private readonly StatsManagementView _view;
    private readonly StatsManagementService _service;
    private readonly Action<IEnumerable<StatData>, int> _onSave;

    public StatsManagementController(
        StatsManagementView view,
        StatsManagementService service,
        Action<IEnumerable<StatData>, int> onSave
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
        _onSave?.Invoke(_service.GetStats(), _service.GetAvailablePoints());
    }

    private void Refresh()
    {
        _view.Refresh(
            _service.GetStats(),
            _service.GetAvailablePoints());
    }
}