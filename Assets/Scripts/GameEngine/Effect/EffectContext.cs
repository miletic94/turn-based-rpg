#nullable enable

using System;
using System.Collections.Generic;

public class EffectContext
{
    public Character Source { get; }
    public Character Target { get; }

    private readonly Dictionary<string, int> _results = new();

    public EffectContext(Character source, Character target)
    {
        Source = source;
        Target = target;
    }

    public Character ResolveTarget(TargetType type)
        => type == TargetType.Self ? Source : Target;

    public void StoreResult(string? id, int value)
    {
        if (id != null)
            _results[id] = value;
    }

    public int GetResult(string id)
    {
        if (!_results.TryGetValue(id, out var value))
            throw new Exception($"Effect result '{id}' not found");

        return value;
    }
}