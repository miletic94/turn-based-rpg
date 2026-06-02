using System;
using Presentation.StatsManagement.StatsPanel;
using UnityEngine;
using UnityEngine.UI;

public class StatManagementView : MonoBehaviour
{
    [SerializeField] ClickableUI _saveButton;
    [SerializeField] StatsPanelView _panel;
    public StatsPanelView Panel => _panel;
    private Action _onSaveClicked;
    private void OnEnable()
    {
        _saveButton.Bind(_onSaveClicked);
    }
    public void BindSaveClicked(Action onSaveClicked)
    {
        _onSaveClicked = onSaveClicked;
    }
}