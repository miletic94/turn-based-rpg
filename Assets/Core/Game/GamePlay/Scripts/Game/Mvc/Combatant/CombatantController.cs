using System.Collections.Generic;
using UnityEngine;

public class CombatantController
{
    private readonly CombatantViewFactory _viewFactory;
    private readonly Dictionary<Character, CombatantView> _combatantViews = new();

    public CombatantController(CombatantViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
    }

    public void Create(Character character)
    {
        var view = _viewFactory.CreateView(character);
        _combatantViews[character] = view;
    }

    public void UpdateHealth(Character character)
    {
        if (_combatantViews.TryGetValue(character, out var view))
        {
            view.UpdateHealthBar(character);
        }
    }

    public void UpdateAll(IEnumerable<Character> characters)
    {
        foreach (var character in characters)
        {

            if (_combatantViews.TryGetValue(character, out var view))
            {
                view.UpdateHealthBar(character);
            }
        }
    }
}