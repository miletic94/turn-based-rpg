using System;
using System.Collections.Generic;

public class LevelTreeViewBinder
{
    private readonly ILevelTreeView _view;
    private readonly ILevelProvider _levelProvider;

    private Action<LevelNodeData> _onLevelSelected;

    public LevelTreeViewBinder(
        ILevelTreeView view,
        ILevelProvider levelProvider)
    {
        _view = view;
        _levelProvider = levelProvider;
    }

    public void Bind(Action<LevelNodeData> onLevelSelected)
    {
        _onLevelSelected = onLevelSelected;

        List<LevelNodeData> levels = _levelProvider.GetAvailableLevels();

        _view.ShowLevels(levels);
        _view.EnableInput(HandleLevelSelected);
    }

    public void Unbind()
    {
        _view.DisableInput();
    }

    private void HandleLevelSelected(LevelNodeData selectedLevel)
    {
        _view.DisableInput();

        _onLevelSelected?.Invoke(selectedLevel);
    }
}