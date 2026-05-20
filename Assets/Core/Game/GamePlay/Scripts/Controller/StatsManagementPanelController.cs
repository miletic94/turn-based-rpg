using System;

public class StatsManagementPanelController
{
    private readonly StatsManagementPanelView _view;
    private readonly StatsManagementService _service;
    private StatManagementData _data;

    public StatsManagementPanelController(
        StatsManagementPanelView view,
        StatsManagementService service)
    {
        _view = view;
        _service = service;
    }

    public void CreateStatPanel(Hero hero, Action<StatSaveData> onSaveButtonClicked)
    {
        _data = CreateStatManagementData(hero);
        RefreshAll();

        _view.SetSaveButtonClickedCallback(
            () => onSaveButtonClicked(CreateStatSaveData(_data)));
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
            _data.AvailableStatPoints.ToString());
    }

    private void RefreshStatRows()
    {
        foreach (var stat in _data.Stats.GetStats())
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
            () => ModifyStat(statType, -1),
            () => ModifyStat(statType, +1)
            );
    }

    private void UpdateRowInteractability(
        StatsManagementPanelRowView row,
        StatData stat)
    {
        bool canDecrease = stat.CurrentGTBase;
        bool canIncrease = _data.AvailableStatPoints > 0;

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
            _service.IncreaseStat(_data, type);
        }
        else
        {
            _service.DecreaseStat(_data, type);
        }

        RefreshAll();
    }
    // ----------------------------
    //  Data
    // ----------------------------

    public StatSaveData CreateStatSaveData(StatManagementData data)
    {
        return new StatSaveData(
            data.Stats.GetStat(StatType.Attack).CurrentValue,
            data.Stats.GetStat(StatType.Defense).CurrentValue,
            data.Stats.GetStat(StatType.Magic).CurrentValue
            );
    }
    public StatManagementData CreateStatManagementData(Hero hero)
    {
        return new StatManagementData(
            hero.AvailableStatPoints,
            new Stats(hero.Attack, hero.Defense, hero.Magic));
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