
public class StatController
{
    private readonly StatView _view;
    private Combatant _currentCharacter;

    public StatController(StatView view)
    {
        _view = view;
    }

    public void Show(Combatant character)
    {
        _currentCharacter = character;
        _view.ShowStat(character);
    }

    public void RefreshStatView(Combatant target, Combatant source)
    {
        if (_currentCharacter == target)
            Refresh(target);
        else if (_currentCharacter == source)
            Refresh(source);
    }

    private void Refresh(Combatant combatant)
    {
        _view.UpdateStat(combatant);
    }
}