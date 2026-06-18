
public class BattleTargetService
{
    public Combatant ResolveTarget(
        BattleContext state,
        Combatant source,
        TargetType targetType)
    {
        return targetType switch
        {
            TargetType.User => source,
            TargetType.Enemy => ResolveDefaultEnemy(state, source),
            _ => ResolveDefaultEnemy(state, source)
        };
    }

    public Combatant ResolveDefaultEnemy(
        BattleContext state,
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