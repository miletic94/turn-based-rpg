using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CombatantPrefabDatabase : ScriptableObject
{
    public List<CombatantPrefabEntry> entries;

    private Dictionary<string, CombatantView> _lookup;

    public void Init()
    {
        _lookup = new Dictionary<string, CombatantView>();

        foreach (var e in entries)
        {
            _lookup[e.id.ToString().ToLowerInvariant()] = e.prefab;
        }
    }

    public CombatantView Get(string id)
    {
        return _lookup[id];
    }
}

[System.Serializable]
public class CombatantPrefabEntry
{
    public CharacterName id;
    public CombatantView prefab;
}

public enum CharacterName
{
    Knight,
    Witch,
    GiantSpider,
    Dragon,
    GoblinWarrior,
    GoblinMage
}