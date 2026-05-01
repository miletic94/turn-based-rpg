public class EnemySelectedEvent : IGameEvent
{
    public Character Enemy { get; }

    public EnemySelectedEvent(Character enemy)
    {
        Enemy = enemy;
    }
}