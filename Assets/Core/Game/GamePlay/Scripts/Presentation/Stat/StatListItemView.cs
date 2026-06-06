using System;
using TMPro;
using UnityEngine;
namespace Presentation.Stat
{
    public class StatListItemView : MonoBehaviour, IListItemView<StatItemData>
    {
        [SerializeField] TMP_Text _label;
        [SerializeField] TMP_Text _value;
        [SerializeField] Color _regularColor;
        [SerializeField] Color _downgradeColor;
        [SerializeField] Color _upgradeColor;
        StatItemData _data;

        public void ShowData(StatItemData data)
        {
            _data = data;
            _label.text = _data.StatType.ToString();
            _value.text = _data.CurrentValue.ToString();
            SetValueColor();
        }
        private void SetValueColor()
        {
            if ((_data.CurrentValue - _data.BaseValue) > 0.01f)
            {
                _upgradeColor.a = 1f;
                _value.color = _upgradeColor;
            }
            else if ((_data.CurrentValue - _data.BaseValue) < -0.01f)
            {
                _downgradeColor.a = 1f;
                _value.color = _downgradeColor;
            }
            else
            {
                _regularColor.a = 1f;
                _value.color = _regularColor;
            }
        }
    }
}
