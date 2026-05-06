using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapView : MonoBehaviour
{
    [SerializeField] private LevelTreeView _levelTreeView;
    [SerializeField] private Button _manageMovesButton;

    public void ShowLevels(List<LevelNodeData> levels, Action<Character> onEnemySelected)
    {
        _levelTreeView.ShowLevels(levels, onEnemySelected);
    }
    public void SetOnMoveManagementButtonClicked(Action onMoveManagementButtonClicked)
    {
        _manageMovesButton.onClick.RemoveAllListeners();
        _manageMovesButton.onClick.AddListener(() => onMoveManagementButtonClicked?.Invoke());
    }
}