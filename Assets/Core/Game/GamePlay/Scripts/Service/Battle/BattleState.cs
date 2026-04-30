using System;
using System.Collections.Generic;

public class BattleState
{
    private readonly List<Character> _combatants;

    public IReadOnlyList<Character> Combatants => _combatants;

    public int CurrentCombatantIndex { get; private set; }

    public int TurnNumber { get; private set; }

    public BattleState(List<Character> combatants)
    {
        if (combatants == null || combatants.Count == 0)
            throw new Exception("Battle requires at least one combatant");

        _combatants = combatants;
        CurrentCombatantIndex = 0;
        TurnNumber = 1;
    }

    public void SetCurrentCombatantIndex(int index)
    {
        CurrentCombatantIndex = index;
    }

    public void IncrementTurn()
    {
        TurnNumber++;
    }
}