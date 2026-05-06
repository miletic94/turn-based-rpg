using System;
using System.Collections.Generic;

public interface ILevelTreeView
{
    void ShowLevels(List<LevelNodeData> levels, Action<Character> onEnemySelected);
}