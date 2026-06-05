using System;
using System.Collections.Generic;

public class MapController
{
    private readonly MapView _mapView;

    public MapController(MapView mapView)
    {
        _mapView = mapView;
    }
    public void Initialize(List<Character> enemies, int levelAchieved, Action<Character> handleEnemySelected,
        Action handleManageMovesButtonClicked)
    {
        var levelNodeDataList = CreateLevelNodeData(enemies, levelAchieved);
        var views = _mapView.ShowLevels(levelNodeDataList);

        foreach (var view in views)
        {
            view.BindClick(handleEnemySelected);
        }
        _mapView.SetOnMoveManagementButtonClicked(handleManageMovesButtonClicked);
    }

    public List<LevelNodeData> CreateLevelNodeData(List<Character> enemies, int levelAchieved)
    {
        var result = new List<LevelNodeData>();
        for (int i = 0; i < enemies.Count; i++)
        {
            var level = i + 1;
            result.Add(new LevelNodeData(
                i,
                enemies[i],
                level,
                levelAchieved < level
            ));
        }
        return result;
    }
}