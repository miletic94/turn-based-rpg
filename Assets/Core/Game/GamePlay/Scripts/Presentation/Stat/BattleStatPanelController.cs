public class BattleStatPanelController
{
    private readonly BattleStatPanelView _battleStatPanelView;
    private Combatant _currentCharacter;

    public BattleStatPanelController(BattleStatPanelView view)
    {
        _battleStatPanelView = view;
    }

    public void Show(Combatant character)
    {
        _currentCharacter = character;

        _battleStatPanelView.SetHeaderText(character.Name);
        CreateHealthRow(character);

        foreach (var stat in character.GetStats())
        {
            CreateStatRow(stat);
        }
    }
    private void CreateHealthRow(Combatant character)
    {
        var healthRow = _battleStatPanelView.CreateHealthRow();
        healthRow.Key.SetText("Health");
        healthRow.Value.SetText(character.Health.ToString());
    }
    private void CreateStatRow(CombatantStatData statData)
    {
        var row = _battleStatPanelView.CreateAndRegisterStatRow(statData.Type);
        row.Key.SetText(statData.Type.ToString());
        row.Value.SetText(statData.GetCurrentValue().ToString());
    }

    public void UpdateHealth(Combatant target, Combatant source)
    {
        if (_currentCharacter == target)
            _battleStatPanelView.UpdateHealth(target.Health.ToString());
        else if (_currentCharacter == source)
            _battleStatPanelView.UpdateHealth(source.Health.ToString());
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
            if (_battleStatPanelView.TryGetStatRow(stat.Type, out var statRow))
            {
                statRow.Value.SetText(stat.GetCurrentValue().ToString());

                var comparison = stat.GetCurrentValue().CompareTo(stat.BaseValue);
                statRow.Value.SetColor(
                    comparison > 0 ? UnityEngine.Color.green :
                    comparison < 0 ? UnityEngine.Color.red :
                    UnityEngine.Color.white
                );
            }
        }
    }
}