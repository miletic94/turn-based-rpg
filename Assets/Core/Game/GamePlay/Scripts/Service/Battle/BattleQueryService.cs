using System;

public class BattleQueryService
{
    public Combatant GetByRole(BattleData state, CombatantRole role)
    {
        foreach (var combatant in state.Combatants)
        {
            if (combatant.Role == role)
                return combatant;
        }

        throw new Exception($"No combatant found with role {role}");
    }

    public Combatant GetPlayer(BattleData state)
    {
        return GetByRole(state, CombatantRole.Player);
    }

    public Combatant GetEnemy(BattleData state)
    {
        return GetByRole(state, CombatantRole.Enemy);
    }
}