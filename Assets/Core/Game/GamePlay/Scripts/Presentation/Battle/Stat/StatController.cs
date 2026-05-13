
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

    public void UpdateHealth(Combatant target, Combatant source)
    {
        if (_currentCharacter == target)
            _view.UpdateHealth(target);
        else if (_currentCharacter == source)
            _view.UpdateHealth(source);
    }

    public void UpdateStatView(Combatant target, Combatant source)
    {
        if (_currentCharacter == target)
            UpdateStats(target);
        else if (_currentCharacter == source)
            UpdateStats(source);
    }

    private void UpdateStats(Combatant combatant)
    {
        _view.UpdateStats(combatant);
    }
}