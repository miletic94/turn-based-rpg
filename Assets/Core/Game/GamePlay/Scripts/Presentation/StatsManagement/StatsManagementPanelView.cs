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
    public StatsManagementPanelRowView CreateAndRegisterStatManagementPanelRow(StatType statType)
    {
        var row = Instantiate(_statManagementPanelRowPrefab, _container);
        _rows[statType] = row;
        return row;
    }
    public bool TryGetStatManagementPanelRow(StatType statType, out StatsManagementPanelRowView statsManagementPanelRowView)
    {
        return _rows.TryGetValue(statType, out statsManagementPanelRowView);
    }

    public void SetSaveButtonClickedCallback(Action onSaveButtonClicked)
    {
        _saveButton.onClick.RemoveAllListeners();
        _saveButton.onClick.AddListener(onSaveButtonClicked.Invoke);
    }
}