#nullable enable

using System;
using System.Collections.Generic;

public class EffectContext
{
    public Combatant Source { get; }
    public Combatant Target { get; }
    private readonly Dictionary<string, float> _results = new();

    public EffectContext(Combatant source, Combatant target)
    {
        Source = source;
        Target = target;
    }

    public Combatant ResolveTarget(TargetType type)
        => type == TargetType.Self ? Source : Target;

    public void StoreResult(string? id, float value)
    {
        if (id != null)
            _results[id] = value;
    }

    public float GetResult(string id)
    {
        if (!_results.TryGetValue(id, out var value))
            throw new Exception($"Effect result '{id}' not found");

        return value;
    }
}