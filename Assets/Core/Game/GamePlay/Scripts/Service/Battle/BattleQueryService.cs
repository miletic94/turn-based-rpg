using System;

public class BattleQueryService
{
    public Character GetByRole(BattleState state, CombatantRole role)
    {
        foreach (var combatant in state.Combatants)
        {
            if (combatant.Role == role)
                return combatant;
        }

        throw new Exception($"No combatant found with role {role}");
    }

    public Character GetPlayer(BattleState state)
    {
        return GetByRole(state, CombatantRole.Player);
    }

    public Character GetEnemy(BattleState state)
    {
        return GetByRole(state, CombatantRole.Enemy);
    }
}