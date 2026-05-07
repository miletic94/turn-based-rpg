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

    public void SetStatName(string statName)
    {
        _statNameText.text = statName;
    }

    public void SetStatValue(string statValue)
    {
        _statValueText.text = statValue;
    }
}