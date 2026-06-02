using System;
using Presentation.Stat;

namespace Presentation.StatsManagement.StatsPanel
{
    public class StatRowViewData : StatItemData
    {
        public bool MinusInteractable { get; }
        public bool PlusInteractable { get; }


        public StatRowViewData(
            int id,
            StatType statType,
            int baseValue,
            int currentValue,
            bool minusInteractable,
            bool plusInteractable) :
                base(id, statType, baseValue, currentValue)
        {
            MinusInteractable = minusInteractable;
            PlusInteractable = plusInteractable;
        }
    }
}