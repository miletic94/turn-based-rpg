using System;

public class StatsManagementPanelController
{
    private readonly StatsManagementPanelView _view;
    private readonly StatsManagementService _service;

    public StatsManagementPanelController(
        StatsManagementPanelView view,
        StatsManagementService service)
    {
        _view = view;
        _service = service;
    }

    public void CreateStatPanel(Action onSaveButtonClicked)
    {
        RefreshAll();

        _view.SetSaveButtonClickedCallback(onSaveButtonClicked);
    }

    // ----------------------------
    // Refresh
    // ----------------------------

    private void RefreshAll()
    {
        RefreshAvailablePoints();
        RefreshStatRows();
    }

    private void RefreshAvailablePoints()
    {
        _view.SetAvailablePointsText(
            _service.GetAvailablePoints().ToString());
    }

    private void RefreshStatRows()
    {
        foreach (var stat in _service.GetStats())
        {
            CreateOrUpdateRow(stat);
        }
    }

    // ----------------------------
    // Row Setup
    // ----------------------------

    private void CreateOrUpdateRow(StatData stat)
    {
        var rowData = CreateStatRowViewData(stat);
        UnityEngine.Debug.Log(
            $"{rowData.BaseValue}, {rowData.CurrentValue}, {rowData.CapValue}");

        var row = _view.UpdateStatRow(rowData);

        if (row == null)
        {
            row = _view.ShowStatRow(rowData);

            BindRowCallbacks(row, stat.Type);
        }

        UpdateRowInteractability(row, stat);
    }

    private void BindRowCallbacks(StatsManagementPanelRowView row, StatType statType)
    {
        row.SetControlCallbacks(
            () => ModifyStat(statType, +1),
            () => ModifyStat(statType, -1));
    }

    private void UpdateRowInteractability(
        StatsManagementPanelRowView row,
        StatData stat)
    {
        bool canDecrease = stat.CurrentGTBase;
        bool canIncrease = _service.GetAvailablePoints() > 0;

        row.SetControlInteractable(
            canDecrease,
            canIncrease);
    }

    // ----------------------------
    // Stat Modification
    // ----------------------------

    private void ModifyStat(StatType type, int delta)
    {
        if (delta > 0)
        {
            _service.IncreaseStat(type);
        }
        else
        {
            _service.DecreaseStat(type);
        }

        RefreshAll();
    }

    // ----------------------------
    // View Data
    // ----------------------------

    private StatRowViewData CreateStatRowViewData(StatData stat)
    {
        return new StatRowViewData(
            stat.Type,
            ConvertToViewValue(stat.BaseValue),
            ConvertToViewValue(stat.CurrentValue));
    }

    private int ConvertToViewValue(float value)
    {
        return (int)(value * 10);
    }
}