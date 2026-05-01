using System;
using System.Collections.Generic;

public interface ILevelTreeView
{
    void ShowLevels(List<LevelNodeData> levels);
    void EnableInput(Action<LevelNodeData> onSelected);
    void DisableInput();
}