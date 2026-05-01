using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelTreeView : MonoBehaviour, ILevelTreeView
{
    [SerializeField] private LevelTreeNodeView _nodePrefab;
    [SerializeField] private Transform _nodesContainer;

    private Action<LevelNodeData> _onSelected;
    private bool _isActive;

    public void ShowLevels(List<LevelNodeData> levels)
    {
        foreach (Transform child in _nodesContainer)
            Destroy(child.gameObject);

        foreach (var level in levels)
        {
            var node = Instantiate(_nodePrefab, _nodesContainer);

            node.SetText(level.LevelNumber.ToString());

            node.SetClickAction(() =>
            {
                if (!_isActive) return;
                _onSelected?.Invoke(level);
            });
        }
    }

    public void EnableInput(Action<LevelNodeData> onSelected)
    {
        _onSelected = onSelected;
        _isActive = true;
    }

    public void DisableInput()
    {
        _onSelected = null;
        _isActive = false;
    }
}