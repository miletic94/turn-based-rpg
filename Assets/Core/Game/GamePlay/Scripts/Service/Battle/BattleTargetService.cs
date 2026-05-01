
public class BattleTargetService
{
    public Combatant ResolveTarget(
        BattleData state,
        Combatant source,
        TargetType targetType)
    {
        return targetType switch
        {
            TargetType.Self => source,
            TargetType.Enemy => ResolveDefaultEnemy(state, source),
            _ => ResolveDefaultEnemy(state, source)
        };
    }

    public Combatant ResolveDefaultEnemy(
        BattleData state,
        Combatant source)
    {
        foreach (var combatant in state.Combatants)
        {
            if (combatant != source)
                return combatant;
        }

        return source;
    }
}