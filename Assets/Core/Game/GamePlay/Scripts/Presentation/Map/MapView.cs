using System;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private ClickableUI _manageMovesButton;
    [SerializeField] private LevelTreeView _list;

    public List<LevelNode> ShowLevels(List<LevelNodeData> levels)
    {
        return _list.Render(levels);
    }
    public void SetOnMoveManagementButtonClicked(Action onMoveManagementButtonClicked)
    {
        _manageMovesButton.Bind(onMoveManagementButtonClicked);
    }
}