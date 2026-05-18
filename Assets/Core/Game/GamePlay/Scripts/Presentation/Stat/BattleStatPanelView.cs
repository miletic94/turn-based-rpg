using System.Collections.Generic;
using UnityEngine;

public class BattleStatPanelView : MonoBehaviour
{
    [SerializeField] private StatHeaderView _statHeader;
    [SerializeField] private StatRowView _statRowPrefab;
    [SerializeField] private Transform _container;
    private StatRowView _healthRow;

    private Dictionary<StatType, StatRowView> _rows = new();

    public void SetHeaderText(string text)
    {
        _statHeader.SetText(text);
    }

    public StatRowView CreateAndRegisterStatRow(StatType statType)
    {
        var row = Instantiate(_statRowPrefab, _container);
        _rows[statType] = row;
        return row;
    }

    public bool TryGetStatRow(StatType statType, out StatRowView statRow)
    {
        return _rows.TryGetValue(statType, out statRow);
    }

    public StatRowView CreateHealthRow()
    {
        _healthRow = Instantiate(_statRowPrefab, _container);
        return _healthRow;
    }

    public void UpdateHealth(string text)
    {
        _healthRow.Value.SetText(text);
    }
}