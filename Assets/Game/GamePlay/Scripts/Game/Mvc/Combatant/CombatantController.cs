using System.Collections.Generic;

public class CombatantController
{
    private readonly Dictionary<Character, CombatantView> _combatantViews = new();

    public void Register(Character character, CombatantView view)
    {
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
            UpdateHealth(character);
        }
    }
}