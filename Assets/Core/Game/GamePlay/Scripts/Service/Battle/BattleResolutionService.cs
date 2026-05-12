public class BattleResolutionService
{
    public bool TryGetWinner(BattleContext state, out Combatant winner)
    {
        int deadIndex = -1;

        for (int i = 0; i < state.Combatants.Count; i++)
        {
            if (state.Combatants[i].Health <= 0)
            {
                deadIndex = i;
                break;
            }
        }

        if (deadIndex != -1)
        {
            int winnerIndex = (deadIndex + 1) % state.Combatants.Count;
            winner = state.Combatants[winnerIndex];
            return true;
        }

        winner = null;
        return false;
    }

    public bool IsBattleOver(BattleContext state)
    {
        return TryGetWinner(state, out _);
    }
}