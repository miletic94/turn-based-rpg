using System;

public class StatsManagementPanelController
{
    private readonly StatsManagementPanelView _statManagementPanelView;
    private readonly StatsManagementService _service;

    public StatsManagementPanelController(
        StatsManagementPanelView view,
        StatsManagementService service
        )
    {
        _statManagementPanelView = view;
        _service = service;
    }

    public void Initialize(Action onSvaeButtonClicked)
    {
        _statManagementPanelView.SetAvailablePointsText(
            _service.GetAvailablePoints().ToString());

        foreach (var statData in _service.GetStats())
        {
            var row
            = _statManagementPanelView.CreateAndRegisterStatManagementPanelRow(statData.Type);
            row.SetRowKey(statData.Type.ToString());
            row.SetRowValue(statData.CurrentValue.ToString());

            row.SetControlInteractable(statData.CurrentGTBase, _service.GetAvailablePoints() > 0);
            row.SetControlCallbacks(
                () => OnMinusClicked(statData.Type),
                () => OnPlusClicked(statData.Type));
        }

        _statManagementPanelView.SetSaveButtonClickedCallback(onSvaeButtonClicked);
    }

    private void Refresh()
    {
        _statManagementPanelView.SetAvailablePointsText(
            _service.GetAvailablePoints().ToString());

        foreach (var stat in _service.GetStats())
        {
            _statManagementPanelView.TryGetStatManagementPanelRow(stat.Type, out var row);
            row.SetRowValue(stat.CurrentValue.ToString());

            row.SetControlInteractable(stat.CurrentGTBase, _service.GetAvailablePoints() > 0);
        }
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
}