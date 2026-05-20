public class StatManagementData
{
    public int AvailableStatPoints { get; private set; }
    public Stats Stats { get; }

    public StatManagementData(int availableStatPoints, Stats stats)
    {
        AvailableStatPoints = availableStatPoints;
        Stats = stats;
    }

    public void SetAvaialableStatPoints(int value)
    {
        AvailableStatPoints = value;
    }
}