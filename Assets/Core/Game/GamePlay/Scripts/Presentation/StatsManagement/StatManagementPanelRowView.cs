using System;
using UnityEngine;
using UnityEngine.UI;

public class StatsManagementPanelRowView : MonoBehaviour
{
    [SerializeField] private StatRowView _statRowView;
    [SerializeField] private Button _minusButton;
    [SerializeField] private Button _plusButton;


    public void SetRowKey(string text)
    {
        _statRowView.Key.SetText(text);
    }
    public void SetRowValue(string text)
    {
        _statRowView.Value.SetText(text);
    }

    public void SetControlInteractable(bool minusInteractable, bool plusInteractable)
    {
        _minusButton.interactable = minusInteractable;
        _plusButton.interactable = plusInteractable;
    }

    public void SetControlCallbacks(Action minusClicked, Action plusClicked)
    {
        _minusButton.onClick.RemoveAllListeners();
        _plusButton.onClick.RemoveAllListeners();

        _minusButton.onClick.AddListener(minusClicked.Invoke);
        _plusButton.onClick.AddListener(plusClicked.Invoke);
    }
}