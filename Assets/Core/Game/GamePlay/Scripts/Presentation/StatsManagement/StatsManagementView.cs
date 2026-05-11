using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManagementView : MonoBehaviour
{
    [SerializeField] private StatAvailablePointsView _availablePointsView;
    [SerializeField] private StatItemRowView _rowPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _saveButton;

    private readonly Dictionary<StatType, StatItemRowView> _rows = new();

    public void Initialize(
        IEnumerable<StatData> stats,
        int availablePoints,
        Action<StatType> onPlus,
        Action<StatType> onMinus,
        Action onSave)
    {
        _availablePointsView.SetAvailablePoints(availablePoints);

        foreach (var statData in stats)
        {
            if (_rows.TryGetValue(statData.Type, out var statView))
            {
                RefreshRow(statData, statView, availablePoints);
            }
            else
            {
                InitializeRow(statData, onPlus, onMinus, availablePoints);
            }
        }

        _saveButton.onClick.RemoveAllListeners();
        _saveButton.onClick.AddListener(onSave.Invoke);
    }

    public void Refresh(IEnumerable<StatData> stats, int availablePoints)
    {
        _availablePointsView.SetAvailablePoints(availablePoints);

        foreach (var stat in stats)
        {
            if (_rows.TryGetValue(stat.Type, out var statView))
            {
                RefreshRow(stat, statView, availablePoints);
            }
            else
            {
                throw new Exception($"Stat {stat.Type} not found");
            }
        }
    }

    private void InitializeRow(
        StatData statData,
        Action<StatType> onPlus,
        Action<StatType> onMinus,
        int availablePoints)
    {
        var row = Instantiate(_rowPrefab, _container);

        row.Initialize(
            statData,
            onPlus,
            onMinus);

        row.Refresh(statData, availablePoints > 0);

        _rows.Add(statData.Type, row);
    }
    private void RefreshRow(StatData stat, StatItemRowView statItem, int availablePoints)
    {
        statItem.Refresh(
            stat,
            availablePoints > 0);
    }
}