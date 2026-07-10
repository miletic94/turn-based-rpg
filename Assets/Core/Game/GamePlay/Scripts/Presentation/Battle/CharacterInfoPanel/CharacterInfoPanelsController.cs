using System.Collections.Generic;
using Presentation.Stat;

public class CharacterInfoPanelsController
{

    private readonly CharacterInfoPanelsView _view;
    private readonly CombatantStatService _combatantStatService;
    private Combatant _player;
    private Combatant _enemy;
    public CharacterInfoPanelsController(CharacterInfoPanelsView view, CombatantStatService combatantStatService)
    {
        _view = view;
        _combatantStatService = combatantStatService;
    }
    public void CreatePanels(Combatant player, Combatant enemy)
    {
        _player = player;
        _enemy = enemy;
        _view.CreatePanels(
            new CharacterInfoPanelData(
                player.Portrait,
                player.Health / player.BaseHealth,
                CreateStatItemData(player.Stats)),
            new CharacterInfoPanelData(
                enemy.Portrait,
                enemy.Health / enemy.BaseHealth,
                CreateStatItemData(enemy.Stats)));
    }

    public void SetHealthBars(Combatant actor, Combatant target)
    {
        SetHealthBar(actor);
        SetHealthBar(target);
    }
    private void SetHealthBar(Combatant combatant)
    {
        if (combatant == _player)
        {
            _view.SetPlayerHealthBar(combatant.Health / combatant.BaseHealth);
        }
        else if (combatant == _enemy)
        {
            _view.SetEnemyHealthBar(combatant.Health / combatant.BaseHealth);
        }
    }
    public void RefreshStatsPanels(Combatant actor, Combatant target)
    {
        RefreshStatsPanel(actor);
        RefreshStatsPanel(target);
    }

    private void RefreshStatsPanel(Combatant combatant)
    {
        if (combatant == _player)
        {
            _view.RefreshPlayerStatsPanel(CreateStatItemData(combatant.Stats));
        }
        else if (combatant == _enemy)
        {
            _view.RefreshEnemyStatsPanel(CreateStatItemData(combatant.Stats));
        }
    }

    private List<StatItemData> CreateStatItemData(CombatantStats stats)
    {
        var statItems = new List<StatItemData>();
        foreach (var stat in stats)
        {
            statItems.Add(CreateStatItemData(stat));
        }
        return statItems;
    }

    private StatItemData CreateStatItemData(CombatantStatData statData)
    {
        return new StatItemData(
                (int)statData.Type,
                statData.Type,
                ViewUtils.ConvertToViewValue(statData.BaseValue),
                ViewUtils.ConvertToViewValue(_combatantStatService.GetStatValue(statData)));
    }
}