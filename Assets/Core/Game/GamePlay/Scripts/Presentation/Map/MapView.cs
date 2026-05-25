using System;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private ClickableUI _manageMovesButton;
    [SerializeField] private LevelTreeView _list;

    public void ShowLevels(List<LevelNodeData> levels)
    {
        _list.Render(levels);
    }
    public LevelNode GetView(int id)
    {
        return _list.GetView(id);
    }
    public void SetOnMoveManagementButtonClicked(Action onMoveManagementButtonClicked)
    {
        _manageMovesButton.Bind(onMoveManagementButtonClicked);
    }
}