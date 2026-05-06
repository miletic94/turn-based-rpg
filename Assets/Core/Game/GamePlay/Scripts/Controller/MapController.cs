using System;
using System.Collections.Generic;

public class MapController
{
    private readonly MapView _mapView;

    public MapController(MapView mapView)
    {
        _mapView = mapView;
    }
    public void Initialize(List<Character> enemies, Action<Character> onEnemySelected,
        Action onManageMovesButtonClicked)
    {
        var availableLevels = GetAvailableLevels(enemies);

        _mapView.ShowLevels(availableLevels, onEnemySelected);
        _mapView.SetOnMoveManagementButtonClicked(onManageMovesButtonClicked);
    }

    public List<LevelNodeData> GetAvailableLevels(List<Character> enemies)
    {
        var result = new List<LevelNodeData>();

        for (int i = 0; i < enemies.Count; i++)
        {
            result.Add(new LevelNodeData
            {
                LevelNumber = i + 1,
                Enemy = enemies[i]
            });
        }

        return result;
    }

}