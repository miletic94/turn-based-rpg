public sealed class BattleFinishedUpdate : BattleUpdate
{
    public Combatant Player { get; }
    public Combatant Enemy { get; }
    public Combatant Winner { get; }

    public BattleFinishedUpdate(Combatant player, Combatant enemy, Combatant winner)
    {
        Player = player;
        Enemy = enemy;
        Winner = winner;
    }
}