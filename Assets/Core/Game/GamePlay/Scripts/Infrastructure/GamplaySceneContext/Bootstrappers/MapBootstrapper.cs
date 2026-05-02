using System;
using System.Collections.Generic;
using UnityEngine;

public class MapBootstrapper : MonoBehaviour
{
    [SerializeField] private MapScreen _mapScreen;
    [SerializeField] private LevelTreeView _levelTreeView;

    private LevelTreeViewBinder _binder;

    public void Initialize(
        List<Character> enemies,
        Action<Character> onEnemySelected,
        Action onManageMovesButtonClicked)
    {
        _binder = new LevelTreeViewBinder(
            _levelTreeView,
            new LevelProvider(enemies));

        _binder.Bind(levelNode =>
        {
            onEnemySelected?.Invoke(levelNode.Enemy);
        });

        _mapScreen.SetOnMoveManagementButtonClicked(onManageMovesButtonClicked);

        _mapScreen.Show();
    }

    public void Unload()
    {
        _binder?.Unbind();
        _mapScreen.Hide();
    }
}