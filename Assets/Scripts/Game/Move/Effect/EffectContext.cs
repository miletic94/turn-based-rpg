#nullable enable

using System;
using System.Collections.Generic;

public class EffectContext
{
    public Character Source { get; }
    public Character Target { get; }
    public List<MoveExecutedEvent> Events = new();

    private readonly Dictionary<string, float> _results = new();

    public EffectContext(Character source, Character target)
    {
        Source = source;
        Target = target;
    }

    public Character ResolveTarget(TargetType type)
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