namespace Presentation.Stat
{
    public class StatItemData : IIdentifiable


    {
        public int Id { get; }
        public StatType StatType { get; }
        public float BaseValue { get; }
        public float CurrentValue { get; }


        public StatItemData(
            int id,
            StatType statType,
            float baseValue,
            float currentValue)
        {
            Id = id;
            StatType = statType;
            BaseValue = baseValue;
            CurrentValue = currentValue;
        }
    }
}
