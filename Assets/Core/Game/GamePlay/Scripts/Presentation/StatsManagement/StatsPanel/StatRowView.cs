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
        public void ShowData(StatRowViewData data)
        {
            base.ShowData(data);

            SetControlCallbacks(
                data.MinusButtonCallback,
                data.PlusButtonCallback);

            SetControlInteractable(data.MinusInteractable, data.PlusInteractable);
        }
        private void SetControlInteractable(bool minusInteractable, bool plusInteractable)
        {
            _minusButton.interactable = minusInteractable;
            _plusButton.interactable = plusInteractable;
        }

        private void SetControlCallbacks(Action minusClicked, Action plusClicked)
        {
            _minusButton.onClick.RemoveAllListeners();
            _plusButton.onClick.RemoveAllListeners();

            _minusButton.onClick.AddListener(minusClicked.Invoke);
            _plusButton.onClick.AddListener(plusClicked.Invoke);
        }
    }
}