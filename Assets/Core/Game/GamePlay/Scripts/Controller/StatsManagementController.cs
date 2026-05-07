using System.Collections.Generic;

public class StatsManagementController
{
    private readonly StatsManagementView _view;
    private Dictionary<StatType, StatItemRowView> _statRows = new();

    public StatsManagementController(StatsManagementView view)
    {
        _view = view;
    }

    public void Initialize(StatsViewData viewData)
    {
        _view.ShowAvailablePoints(viewData.AvailablePoints);
        _view.SetAvailablePoints(viewData.AvailablePoints);
        foreach (var stat in viewData.GetStats())
        {
            var row = _view.ShowStatItemRow(stat.type.ToString(), stat.currentValue.ToString());
            _statRows[stat.type] = row;
        }
    }
}