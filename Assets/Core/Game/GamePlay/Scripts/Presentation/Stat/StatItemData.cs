namespace Presentation.Stat
{
    public class StatItemData : IIdentifiable


    {
        public int Id { get; }
        public StatType StatType { get; }
        public int BaseValue { get; }
        public int CurrentValue { get; }


        public StatItemData(
            int id,
            StatType statType,
            int baseValue,
            int currentValue)
        {
            Id = id;
            StatType = statType;
            BaseValue = baseValue;
            CurrentValue = currentValue;
        }
    }
}
