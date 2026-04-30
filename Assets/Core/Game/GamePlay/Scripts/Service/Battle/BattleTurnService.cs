using System;

public class BattleTurnService
{
    public Character GetCurrentCombatant(BattleState state)
    {
        return state.Combatants[state.CurrentCombatantIndex];
    }

    public Character GetNextCombatant(BattleState state)
    {
        int nextIndex = (state.CurrentCombatantIndex + 1) % state.Combatants.Count;
        return state.Combatants[nextIndex];
    }

    public void AdvanceTurn(BattleState state)
    {
        if (state.Combatants.Count == 0)
            throw new Exception("No combatants");

        int nextIndex = (state.CurrentCombatantIndex + 1) % state.Combatants.Count;

        state.SetCurrentCombatantIndex(nextIndex);

        if (nextIndex == 0)
            state.IncrementTurn();
    }
}