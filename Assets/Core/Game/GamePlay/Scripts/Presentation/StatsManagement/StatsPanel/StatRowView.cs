using System;
using Presentation.Stat;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.StatsManagement.StatsPanel
{
    public class StatRowView : StatListItemView, IListItemView<StatRowViewData>
    {
        [SerializeField] private Button _minusButton;
        [SerializeField] private Button _plusButton;
        private StatType _statType;
        public void ShowData(StatRowViewData data)
        {
            base.ShowData(data);
            _statType = data.StatType;

            SetControlInteractable(data.MinusInteractable, data.PlusInteractable);
        }
        private void SetControlInteractable(bool minusInteractable, bool plusInteractable)
        {
            _minusButton.interactable = minusInteractable;
            _plusButton.interactable = plusInteractable;
        }

        public void SetControlCallbacks(Action<StatType, int> minusClicked, Action<StatType, int> plusClicked)
        {
            _minusButton.onClick.RemoveAllListeners();
            _plusButton.onClick.RemoveAllListeners();

            _minusButton.onClick.AddListener(() => minusClicked.Invoke(_statType, -1));
            _plusButton.onClick.AddListener(() => plusClicked.Invoke(_statType, 1));
        }
    }
}