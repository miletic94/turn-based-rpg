using System;
using System.Collections.Generic;

public class BattleState
{
    private List<Character> _combatants;
    private int _currentCombatantIndex;
    public int TurnNumber { get; private set; }

    public BattleState(List<Character> combatants)
    {
        _combatants = combatants;
        _currentCombatantIndex = 0;
        TurnNumber = 1;
    }

    public void NextTurn()
    {
        if (_combatants.Count == 0)
            throw new Exception("No combantants");

        _currentCombatantIndex = (_currentCombatantIndex + 1) % _combatants.Count;
        TurnNumber++;
    }

    public Character GetSourceCombatant()
    {
        return _combatants[_currentCombatantIndex];
    }
    public Character GetTargetCombatant()
    {
        return _combatants[(_currentCombatantIndex + 1) % _combatants.Count];
    }

    public bool TryEnd(out Character winner)
    {
        int deadIndex = _combatants.FindIndex(comb => comb.Health <= 0);
        if (deadIndex != -1)
        {
            int winnerIndex = (deadIndex + 1) % _combatants.Count;
            winner = _combatants[winnerIndex];
            return true;
        }
        winner = null;
        return false;
    }
}