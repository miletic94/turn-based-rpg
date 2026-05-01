using System.Collections.Generic;

public class CombatantViewBinder
{
    private readonly CombatantViewFactory _factory;
    private readonly Dictionary<Character, CombatantView> _views = new();
    private readonly EventBus _bus;

    public CombatantViewBinder(
        CombatantViewFactory factory,
        EventBus bus)
    {
        _factory = factory;
        _bus = bus;
    }

    public void Register(Character character)
    {
        var view = _factory.CreateView(character);
        _views[character] = view;
    }

    public void UnregisterAll()
    {
        foreach (var view in _views.Values)
            view.Dispose();

        _views.Clear();
    }

    public void Bind()
    {
        _bus.Subscribe<MoveExecutedEvent>(OnMoveExecuted);
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