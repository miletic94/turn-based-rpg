public class BattleStatPanelController
{
    private readonly BattleStatPanelView _battleStatPanelView;
    private Combatant _currentCharacter;

    public BattleStatPanelController(BattleStatPanelView view)
    {
        _battleStatPanelView = view;
    }

    public void CreateStatPanel(Combatant character)
    {
        _currentCharacter = character;

        _battleStatPanelView.SetHeaderText(character.Name);

        ShowStats(character);
    }

    public void SetHealthBar(Combatant target, Combatant source)
    {
        if (_currentCharacter == target)
            _battleStatPanelView.SetHealthBar(target.Health / target.BaseHealth);
        else if (_currentCharacter == source)
            _battleStatPanelView.SetHealthBar(source.Health / source.BaseHealth);
    }

    public void ShowStats(Combatant character)
    {
        foreach (var stat in character.GetStats())
        {
            _battleStatPanelView.ShowStatRow(CreateStatRowViewData(stat));
        }
    }

    public void UpdateStats(Combatant target, Combatant source)
    {
        if (_currentCharacter == target)
            UpdateStats(target);
        else if (_currentCharacter == source)
            UpdateStats(source);
    }

    private void UpdateStats(Combatant combatant)
    {
        foreach (var stat in combatant.GetStats())
        {
            _battleStatPanelView.UpdateStatRow(CreateStatRowViewData(stat));
        }
    }

    public StatRowViewData CreateStatRowViewData(CombatantStatData combatantStatData)
    {
        int baseValueView = (int)(combatantStatData.BaseValue * 10);
        int currentValueView = (int)(combatantStatData.GetCurrentValue() * 10);

        return new StatRowViewData(
            combatantStatData.Type,
            baseValueView,
            currentValueView);
    }
}