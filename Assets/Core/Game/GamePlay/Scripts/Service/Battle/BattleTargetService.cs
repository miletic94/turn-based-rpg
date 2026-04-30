
public class BattleTargetService
{
    public Character ResolveTarget(
        BattleData state,
        Character source,
        TargetType targetType)
    {
        return targetType switch
        {
            TargetType.Self => source,
            TargetType.Enemy => ResolveDefaultEnemy(state, source),
            _ => ResolveDefaultEnemy(state, source)
        };
    }

    public Character ResolveDefaultEnemy(
        BattleData state,
        Character source)
    {
        foreach (var combatant in state.Combatants)
        {
            if (combatant != source)
                return combatant;
        }

        return source;
    }
}