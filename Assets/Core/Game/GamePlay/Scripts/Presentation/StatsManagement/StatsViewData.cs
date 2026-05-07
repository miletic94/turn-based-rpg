using System.Collections.Generic;

public class StatsViewData
{
    public float Attack { get; private set; }
    public float Defense { get; private set; }
    public float Magic { get; private set; }
    public int AvailablePoints { get; private set; }
    public StatsViewData(Hero hero)
    {
        Attack = hero.Attack;
        Defense = hero.Defense;
        Magic = hero.Magic;
        AvailablePoints = hero.AvailableStatPoints;
    }

    // TODO: Should this exist? Maybe we should have a separate service that handles leveling up and stat point allocation, and then just pass the updated hero to the view?
    public IEnumerable<(StatType type, float currentValue)> GetStats()
    {
        yield return (StatType.Attack, Attack);
        yield return (StatType.Defense, Defense);
        yield return (StatType.Magic, Magic);
    }
}