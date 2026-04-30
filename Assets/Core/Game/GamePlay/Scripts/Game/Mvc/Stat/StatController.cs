public class StatController
{
    private readonly StatView _statView;
    private Character _currentCharacter;

    public StatController(StatView statView)
    {
        _statView = statView;
    }

    public void Show(Character character)
    {
        _currentCharacter = character;
        _statView.ShowStat(character);
    }

    public void Update(Character character)
    {
        if (_currentCharacter == character)
        {
            _statView.UpdateStat(character);
        }
    }
}