using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagementView : MonoBehaviour
{
    [SerializeField] private StatAvailablePointsView _availablePointsView;
    [SerializeField] private StatItemRowView _rowPrefab;
    [SerializeField] private Transform _container;

    private readonly Dictionary<StatType, StatItemRowView> _rows = new();

    public void Initialize(
        IEnumerable<StatData> stats,
        int availablePoints,
        Action<StatType> onPlus,
        Action<StatType> onMinus)
    {
        _availablePointsView.SetAvailablePoints(availablePoints);

        foreach (var stat in stats)
        {
            var row = Instantiate(_rowPrefab, _container);

            row.Initialize(
                stat,
                onPlus,
                onMinus);

            _rows.Add(stat.Type, row);
        }
    }

    public void Refresh(IEnumerable<StatData> stats, int availablePoints)
    {
        _availablePointsView.SetAvailablePoints(availablePoints);

        foreach (var stat in stats)
        {
            _rows[stat.Type].Refresh(
                stat,
                availablePoints > 0);
        }
    }
}