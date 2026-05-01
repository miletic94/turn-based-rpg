using System.Collections.Generic;

public interface ILevelProvider
{
    List<LevelNodeData> GetAvailableLevels();
}