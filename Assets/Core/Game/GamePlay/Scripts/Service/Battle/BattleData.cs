using System;
using System.Collections.Generic;

public class BattleData
{
    private readonly List<Character> _combatants;

    public IReadOnlyList<Character> Combatants => _combatants;

    public int CurrentCombatantIndex { get; private set; }

    public int TurnNumber { get; private set; }

    public BattleData(List<Character> combatants)
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