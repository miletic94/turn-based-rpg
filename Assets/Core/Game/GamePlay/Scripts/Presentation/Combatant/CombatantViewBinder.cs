using System.Collections.Generic;
using UnityEngine;

public class CombatantViewBinder
{
    private readonly CombatantViewFactory _factory;
    private readonly Dictionary<Character, CombatantView> _views = new();

    public CombatantViewBinder(
        CombatantViewFactory factory,
        EventBus bus)
    {
        _factory = factory;
    }

    public void Register(Character character)
    {
        var view = _factory.CreateView(character);
        _views[character] = view;
    }

    public void Bind(EventBus bus)
    {
        bus.Subscribe<MoveExecutedEvent>(OnMoveExecuted);
    }

    public void Unbind(EventBus bus)
    {
        bus.Unsubscribe<MoveExecutedEvent>(OnMoveExecuted);
    }

    private void OnMoveExecuted(MoveExecutedEvent e)
    {
        Update(e.Source);
        Update(e.Target);
    }

    private void Update(Character character)
    {
        if (_views.TryGetValue(character, out var view))
            view.UpdateHealthBar(character);
    }
}