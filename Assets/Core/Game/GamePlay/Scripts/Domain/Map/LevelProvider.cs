using System.Collections.Generic;

public class LevelProvider : ILevelProvider
{
    private readonly List<Character> _enemies;

    public LevelProvider(List<Character> enemies)
    {
        _enemies = enemies;
    }

    public List<LevelNodeData> GetAvailableLevels()
    {
        var result = new List<LevelNodeData>();

        for (int i = 0; i < _enemies.Count; i++)
        {
            result.Add(new LevelNodeData
            {
                LevelNumber = i + 1,
                Enemy = _enemies[i]
            });
        }

        return result;
    }
}