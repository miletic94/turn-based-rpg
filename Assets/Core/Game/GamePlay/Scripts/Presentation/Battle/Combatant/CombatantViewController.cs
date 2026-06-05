using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatantViewController
{
    private readonly CombatantViewFactory _factory;
    private readonly Dictionary<Combatant, CombatantView> _views = new();

    public CombatantViewController(CombatantViewFactory factory)
    {
        _factory = factory;
    }
    public async Awaitable ShowMoveResult(List<IEffectResult> moveResult)
    {
        var groupedResults = moveResult.GroupBy(result => result.Target);
        foreach (var group in groupedResults)
        {
            var combatant = group.Key;
            var view = _views[combatant];

            var telegraphs = group
                .Select(CreateMoveTelegraphData)
                .ToList();

            await view.ShowData(telegraphs);
        }
    }
    private MoveTelegraphData CreateMoveTelegraphData(IEffectResult moveResult)
    {

        var (text, color) = moveResult switch
        {
            DamageEffectResult result =>
                ($"{result.Value}", Color.red),

            HealEffectResult result =>
                ($"{result.Value}", Color.green),

            StatModifierEffectResult result =>
                (
                    CreateStatusModifierEffectResultString(result),
                    result.Modifier.Type == ModifierType.Buff
                        ? Color.green
                        : Color.red
                ),

            _ => ("", Color.white)
        };

        return new MoveTelegraphData(text, color);
    }
    private string CreateStatusModifierEffectResultString(StatModifierEffectResult result)
    {
        var modifier = result.Modifier;
        var isBuff = modifier.Type == ModifierType.Buff;
        var value = modifier.Value * 10;
        return $"{modifier.Stat} {(isBuff ? value : -value)}";
    }

    public void Create(Combatant combatant)
    {
        var view = _factory.CreateView(combatant);
        _views[combatant] = view;
    }

    public void RemoveAll()
    {
        foreach (var view in _views.Values)
            view.Dispose();

        _views.Clear();
    }
}