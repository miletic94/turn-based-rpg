using System.Collections.Generic;

public class CombatantViewController
{
    private readonly CombatantViewFactory _factory;
    private readonly Dictionary<Combatant, CombatantView> _views = new();

    public CombatantViewController(CombatantViewFactory factory)
    {
        _factory = factory;
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