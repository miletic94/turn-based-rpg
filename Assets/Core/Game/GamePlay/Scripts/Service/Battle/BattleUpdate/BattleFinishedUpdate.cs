public sealed class BattleFinishedUpdate : BattleUpdate
{
    public Combatant Winner { get; }

    public BattleFinishedUpdate(Combatant winner)
    {
        Winner = winner;
    }
}