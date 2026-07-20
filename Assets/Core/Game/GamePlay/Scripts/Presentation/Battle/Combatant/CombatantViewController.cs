using System.Collections.Generic;
using UnityEngine;

public class CombatantViewController
{
    private readonly CombatantViewFactory _factory;
    private readonly Dictionary<Combatant, CombatantView> _views = new();
    private readonly Color _green =
    new(22f / 255f, 130f / 255f, 21f / 255f, 1f);

    private readonly Color _red =
        new(1f, 0f, 0f, 1f);

    private Color GetModifierColor(float value)
    {
        return value > 0 ? _green : _red;
    }

    public CombatantViewController(CombatantViewFactory factory)
    {
        _factory = factory;
    }
    public async Awaitable ShowMoveEffect(MoveEffect moveEffect)
    {
        foreach (var healthModifier in moveEffect.HealthModifierEffects)
        {
            var data = CreateHealthModifierTelegraph(healthModifier);
            await ShowTelegraph(healthModifier.Target, data);
        }
        foreach (var statModifier in moveEffect.StatModifierEffects)
        {
            var data = CreateStathModifierTelegraph(statModifier);
            await ShowTelegraph(statModifier.Target, data);
        }
    }
    private async Awaitable ShowTelegraph(
    Combatant target,
    MoveTelegraphData data)
    {
        var view = _views[target];
        await view.ShowData(data);
    }
    private MoveTelegraphData CreateHealthModifierTelegraph(
        HealthModifierEffect effect)
    {
        var text = effect.Value.ToString();
        var color = GetModifierColor(effect.Value);

        return new MoveTelegraphData(text, color);
    }

    private MoveTelegraphData CreateStathModifierTelegraph(
        StatModifierEffect effect)
    {
        var value = effect.ActiveModifier.Value;
        var text = $"{effect.ActiveModifier.TargetStat} {ViewUtils.ConvertToViewValue(value)}";
        var color = GetModifierColor(value);

        return new MoveTelegraphData(text, color);
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