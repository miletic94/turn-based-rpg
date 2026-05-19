using System.Collections.Generic;
using UnityEngine;

public class BattleStatPanelView : MonoBehaviour
{
    [SerializeField] private StatHeaderView _statHeader;
    [SerializeField] private StatRowView _statRowPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private HealthBarView _healthBarView;

    private Dictionary<StatType, StatRowView> _rows = new();

    public void SetHeaderText(string text)
    {
        _statHeader.SetText(text);
    }

    public void SetHealthBar(float percent)
    {
        _healthBarView.SetImmediate(percent);
    }

    public StatRowView ShowStatRow(StatRowViewData data)
    {
        var row = Instantiate(_statRowPrefab, _container);
        row.ShowStatRow(data);
        _rows[data.StatType] = row;
        return row;
    }

    public void UpdateStatRow(StatRowViewData statRowViewData)
    {
        if (_rows.TryGetValue(statRowViewData.StatType, out var statRowView))
        {
            statRowView.ShowStatRow(statRowViewData);
        }
    }
}