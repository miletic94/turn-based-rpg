using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatsManagementPanelView : MonoBehaviour
{
    [SerializeField] private StatAvailablePointsView _availablePointsView;
    [SerializeField] private Transform _container;
    [SerializeField]
    private StatsManagementPanelRowView _statManagementPanelRowPrefab;
    [SerializeField] private Button _saveButton;

    private readonly Dictionary<StatType, StatsManagementPanelRowView> _rows = new();

    public void SetAvailablePointsText(string text)
    {
        _availablePointsView.Value.SetText(text);
    }

    public StatsManagementPanelRowView ShowStatRow(StatRowViewData data)
    {
        var row = Instantiate(_statManagementPanelRowPrefab, _container);
        row.ShowStatRow(data);
        _rows[data.StatType] = row;
        return row;
    }

    public StatsManagementPanelRowView UpdateStatRow(StatRowViewData statRowViewData)
    {
        if (_rows.TryGetValue(statRowViewData.StatType, out var statManagementRowView))
        {
            statManagementRowView.ShowStatRow(statRowViewData);
        }
        return statManagementRowView;
    }

    public void SetSaveButtonClickedCallback(Action onSaveButtonClicked)
    {
        _saveButton.onClick.RemoveAllListeners();
        _saveButton.onClick.AddListener(onSaveButtonClicked.Invoke);
    }
}