using System.Collections.Generic;

// TODO: Maybe rename to CombatantUIController
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

        // Initial sync
        view.UpdateHealthBar(combatant);
    }

    public void RemoveAll()
    {
        foreach (var view in _views.Values)
            view.Dispose();

        _views.Clear();
    }

    public void OnMoveExecuted(Combatant source, Combatant target, Move move)
    {
        Update(source);
        Update(target);
    }

    private void Update(Combatant combatant)
    {
        if (_views.TryGetValue(combatant, out var view))
        {
            view.UpdateHealthBar(combatant);
        }
    }
}