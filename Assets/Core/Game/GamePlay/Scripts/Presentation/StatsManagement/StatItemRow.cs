using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO: Compare with StatRowView in Battle Stat
public class StatItemRowView : MonoBehaviour
{
    [SerializeField] private TMP_Text _statNameText;
    [SerializeField] private TMP_Text _statValueText;
    [SerializeField] private Button _minusButton;
    [SerializeField] private Button _plusButton;

    public void Initialize(
        StatData stat,
        Action<StatType> onPlus,
        Action<StatType> onMinus)
    {
        _plusButton.onClick.AddListener(() => onPlus(stat.Type));
        _minusButton.onClick.AddListener(() => onMinus(stat.Type));

        Refresh(stat, true);
    }

    public void Refresh(StatData stat, bool canIncrease)
    {
        SetStatName(stat.Type.ToString());
        SetStatValue(stat.CurrentValue.ToString());
        Color color = stat.CurrentGTBase ? Color.green : Color.white;
        SetStatColor(color);

        SetButtonsInteractable(stat.CurrentGTBase, canIncrease);
    }

    public void SetStatName(string statName)
    {
        _statNameText.text = statName;
    }

    public void SetStatValue(string statValue)
    {
        _statValueText.text = statValue;
    }

    public void SetStatColor(Color color)
    {
        _statValueText.color = color;
    }
    public void SetButtonCallbacks(Action minusButtonCb, Action plusButtonCb)
    {
        _minusButton.onClick.AddListener(minusButtonCb.Invoke);
        _plusButton.onClick.AddListener(plusButtonCb.Invoke);
    }
    public void SetButtonsInteractable(bool minusInteractable, bool plusInteractable)
    {
        _minusButton.interactable = minusInteractable;
        _plusButton.interactable = plusInteractable;
    }
}