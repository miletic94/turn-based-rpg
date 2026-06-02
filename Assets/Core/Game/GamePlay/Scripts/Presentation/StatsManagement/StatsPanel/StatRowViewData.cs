using System;
using Presentation.Stat;

namespace Presentation.StatsManagement.StatsPanel
{
    public class StatRowViewData : StatItemData
    {
        public Action MinusButtonCallback { get; }
        public Action PlusButtonCallback { get; }
        public bool MinusInteractable { get; }
        public bool PlusInteractable { get; }


        public StatRowViewData(
            int id,
            StatType statType,
            int baseValue,
            int currentValue,
            Action minusButtonCallback,
            Action plusButtonCallback,
            bool minusInteractable,
            bool plusInteractable) :
                base(id, statType, baseValue, currentValue)
        {
            MinusButtonCallback = minusButtonCallback;
            PlusButtonCallback = plusButtonCallback;
            MinusInteractable = minusInteractable;
            PlusInteractable = plusInteractable;
        }
    }
}