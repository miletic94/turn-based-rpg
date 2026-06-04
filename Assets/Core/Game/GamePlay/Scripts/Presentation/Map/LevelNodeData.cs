public class LevelNodeData : IIdentifiable
{
    public int Id { get; }
    public Character Enemy { get; }
    public int LevelNumber { get; }
    public LevelNodeData(int id, Character enemy, int levelNumber)
    {
        Id = id;
        Enemy = enemy;
        LevelNumber = levelNumber;
    }
}