
public class BattleTargetService
{
    public Character ResolveTarget(
        BattleState state,
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
        BattleState state,
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