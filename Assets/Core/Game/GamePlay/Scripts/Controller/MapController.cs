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
        // LevelData = {int Id, int LevelNumber, Character Enemy} - It comes from database
        var levelDataList = CreateLeveData(enemies);
        // LevelNodeData = {int Id, int LevelNumber} - created from level data
        var levelNodDataList = CreateLevelNodeData(levelDataList);
        // So here level data and level node data are fully synced
        _mapView.ShowLevels(levelNodDataList);

        foreach (var level in levelDataList)
        {
            // We can do this becuase level data and level node data are synced
            var view = _mapView.GetView(level.Id);
            view.BindClick(() => handleEnemySelected.Invoke(level.Enemy));
        }
        _mapView.SetOnMoveManagementButtonClicked(handleManageMovesButtonClicked);
    }

    public List<LevelData> CreateLeveData(List<Character> enemies)
    {
        var result = new List<LevelData>();

        for (int i = 0; i < enemies.Count; i++)
        {
            result.Add(new LevelData
            {
                Id = i,
                LevelNumber = i + 1,
                Enemy = enemies[i]
            });
        }
        return result;
    }

    public List<LevelNodeData> CreateLevelNodeData(List<LevelData> levels)
    {
        var result = new List<LevelNodeData>();

        foreach (var level in levels)
        {
            result.Add(new LevelNodeData
            {
                Id = level.Id,
                LevelNumber = level.LevelNumber,
            });
        }

        return result;
    }

}