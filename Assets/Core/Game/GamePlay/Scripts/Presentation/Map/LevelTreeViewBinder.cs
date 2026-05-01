using System.Collections.Generic;

public class LevelTreeViewBinder
{
    private readonly ILevelTreeView _view;
    private readonly GameplayStateMachine _stateMachine;
    private readonly Character _player;
    private readonly ILevelProvider _levelProvider;

    public LevelTreeViewBinder(
        ILevelTreeView view,
        GameplayStateMachine stateMachine,
        Character player,
        ILevelProvider levelProvider)
    {
        _view = view;
        _stateMachine = stateMachine;
        _player = player;
        _levelProvider = levelProvider;
    }

    public void Bind()
    {
        List<LevelNodeData> levels = _levelProvider.GetAvailableLevels();

        _view.ShowLevels(levels);
        _view.EnableInput(OnLevelSelected);
    }

    public void Unbind()
    {
        _view.DisableInput();
    }

    private void OnLevelSelected(LevelNodeData selectedLevel)
    {
        _view.DisableInput();

        Character enemy = selectedLevel.Enemy;

        _stateMachine.EnterBattle(_player, enemy);
    }
}