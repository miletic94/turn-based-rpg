public class LevelNodeData : IIdentifiable
{
    public int Id { get; }
    public Character Enemy { get; }
    public int LevelNumber { get; }
    public bool Interactable { get; }
    public LevelNodeData(int id, Character enemy, int levelNumber, bool interactable)
    {
        Id = id;
        Enemy = enemy;
        Interactable = interactable;
        LevelNumber = levelNumber;
    }
}