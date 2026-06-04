using System;
using System.Collections.Generic;

public class MapController
{
    private readonly MapView _mapView;

    public MapController(MapView mapView)
    {
        _mapView = mapView;
    }
    public void Initialize(List<Character> enemies, Action<Character> handleEnemySelected,
        Action handleManageMovesButtonClicked)
    {
        var levelNodeDataList = CreateLevelNodeData(enemies);
        var views = _mapView.ShowLevels(levelNodeDataList);

        foreach (var view in views)
        {
            view.BindClick(handleEnemySelected);
        }
        _mapView.SetOnMoveManagementButtonClicked(handleManageMovesButtonClicked);
    }

    public List<LevelNodeData> CreateLevelNodeData(List<Character> enemies)
    {
        var result = new List<LevelNodeData>();
        for (int i = 0; i < enemies.Count; i++)
        {
            result.Add(new LevelNodeData(
                i,
                enemies[i],
                i + 1
            ));
        }
        return result;
    }
}