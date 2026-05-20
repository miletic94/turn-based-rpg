public class StatRowViewData
{
    public StatType StatType;
    public int BaseValue;
    public int CurrentValue;
    // TODO: Rethink cap vale (maybe it shouldn't exist)
    public int CapValue;
    public StatRowViewData(StatType statType, int baseValue, int currentValue, int capValue = 20)
    {
        StatType = statType;
        BaseValue = baseValue;
        CurrentValue = currentValue;
        CapValue = capValue;
    }
}