public class LevelNodeData : IIdentifiable
{
    public int Id { get; }
    public Character Enemy { get; }
    public int LevelNumber { get; }
    public LevelNodeData(int id, Character enemey, int levelNumber)
    {
        Id = id;
        Enemy = Enemy;
        LevelNumber = levelNumber;
    }
}