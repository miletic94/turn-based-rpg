using TMPro;
using UnityEngine;
namespace Presentation.Stat
{
    public class StatListItemView : MonoBehaviour, IListItemView<StatItemData>
    {
        [SerializeField] TMP_Text _label;
        [SerializeField] TMP_Text _value;
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
            _value.color = _data.BaseValue < _data.CurrentValue ? Color.green : Color.red;
        }
    }
}