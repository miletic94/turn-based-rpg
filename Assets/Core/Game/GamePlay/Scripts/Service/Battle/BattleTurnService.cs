using System;

public class BattleTurnService
{
    public Combatant GetCurrentCombatant(BattleContext state)
    {
        return state.Combatants[state.CurrentCombatantIndex];
    }

    public Combatant GetNextCombatant(BattleContext state)
    {
        int nextIndex = (state.CurrentCombatantIndex + 1) % state.Combatants.Count;
        return state.Combatants[nextIndex];
    }

    public void AdvanceTurn(BattleContext state)
    {
        if (state.Combatants.Count == 0)
            throw new Exception("No combatants");

        int nextIndex = (state.CurrentCombatantIndex + 1) % state.Combatants.Count;

        state.SetCurrentCombatantIndex(nextIndex);

        if (nextIndex == 0)
            state.IncrementTurn();
    }
}