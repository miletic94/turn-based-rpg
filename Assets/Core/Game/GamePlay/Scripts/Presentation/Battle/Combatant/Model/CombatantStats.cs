using System.Collections;
using System.Collections.Generic;

public class CombatantStats : IEnumerable<CombatantStatData>
{
    private readonly Dictionary<StatType, CombatantStatData> _stats;
    public CombatantStats(float attack, float defense, float magic)
    {
        _stats = new Dictionary<StatType, CombatantStatData>
        {
            {
                StatType.Attack,
                new CombatantStatData(
                    StatType.Attack,
                    attack,
                    new List<ActiveModifier>())
            },
            {
                StatType.Defense,
                new CombatantStatData(
                    StatType.Defense,
                    defense,
                    new List<ActiveModifier>())
            },
            {
                StatType.Magic,
                new CombatantStatData(
                    StatType.Magic,
                    magic,
                    new List<ActiveModifier>())
            }
        };
    }

    #region IEnumerable and indexer
    public CombatantStatData this[StatType statType]
    {
        get => _stats[statType];
    }
    public IEnumerator<CombatantStatData> GetEnumerator()
    {
        return _stats.Values.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion
}