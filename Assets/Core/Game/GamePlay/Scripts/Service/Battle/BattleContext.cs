using System;
using System.Collections.Generic;

public class BattleContext
{
    private readonly List<Combatant> _combatants;

    public IReadOnlyList<Combatant> Combatants => _combatants;

    private int _currentCombatantIndex;
    private int NextCombatantIndex => (_currentCombatantIndex + 1) % _combatants.Count;

    public int TurnNumber { get; private set; }
    public Combatant CurrentActor => _combatants[_currentCombatantIndex];
    public Combatant CurrentTarget => _combatants[NextCombatantIndex];

    public BattleContext(List<Combatant> combatants)
    {
        if (combatants == null || combatants.Count == 0)
            throw new Exception("Battle requires at least one combatant");

        _combatants = combatants;
        _currentCombatantIndex = 0;
        TurnNumber = 1;
    }

    public void AdvanceTurn()
    {
        if (_combatants.Count == 0)
            throw new Exception("No combatants");

        int nextIndex = NextCombatantIndex;

        _currentCombatantIndex = nextIndex;

        if (nextIndex == 0)
            TurnNumber++;
    }
}