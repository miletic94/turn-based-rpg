using TMPro;
using UnityEngine;

public class StatRowView : MonoBehaviour
{
    [SerializeField] TMP_Text _label;
    [SerializeField] StatValueBarView _statValueBarView;

    public void ShowStatRow(StatRowViewData data)
    {
        _label.text = data.StatType.ToString();
        _statValueBarView.ShowValueBar(data.BaseValue, data.CurrentValue, data.CapValue);
    }
}