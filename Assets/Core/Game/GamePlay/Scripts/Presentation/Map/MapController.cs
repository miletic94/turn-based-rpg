using System;
using System.Collections.Generic;
using System.Diagnostics;

public class MapController
{
    private readonly MapView _mapView;

    public MapController(MapView mapView)
    {
        _mapView = mapView;
    }
    public void Initialize(List<Character> enemies, List<string> enemiesBeaten, Action<Character> handleEnemySelected,
        Action handleManageMovesButtonClicked)
    {
        var levelNodeDataList = CreateLevelNodeData(enemies, enemiesBeaten);
        var views = _mapView.ShowLevels(levelNodeDataList);

        foreach (var view in views)
        {
            view.BindClick(handleEnemySelected);
        }
        _mapView.SetOnMoveManagementButtonClicked(handleManageMovesButtonClicked);
    }

    public List<LevelNodeData> CreateLevelNodeData(List<Character> enemies, List<string> enemiesBeaten)
    {
        var result = new List<LevelNodeData>();
        foreach (var name in enemiesBeaten)
        {
            UnityEngine.Debug.Log(name);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            var level = i + 1;
            result.Add(new LevelNodeData(
                i,
                enemies[i],
                level,
                !enemiesBeaten.Contains(enemies[i].Name)
            ));
        }
        return result;
    }
}