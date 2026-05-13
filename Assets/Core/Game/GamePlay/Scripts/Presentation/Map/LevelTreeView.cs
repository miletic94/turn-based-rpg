using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelTreeView : MonoBehaviour, ILevelTreeView
{
    [SerializeField] private LevelTreeNodeView _nodePrefab;
    [SerializeField] private Transform _nodesContainer;

    public void ShowLevels(List<LevelNodeData> levels, Action<Character> onEnemySelected)
    {
        foreach (Transform child in _nodesContainer)
            Destroy(child.gameObject);

        foreach (var level in levels)
        {
            var node = Instantiate(_nodePrefab, _nodesContainer);

            node
                .SetInteractable(false)
                .SetText(level.LevelNumber.ToString())
                .OnClicked(() => onEnemySelected(level.Enemy));
        }
    }
}