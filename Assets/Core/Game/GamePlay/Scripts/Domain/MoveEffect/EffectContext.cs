using System;
using System.Collections.Generic;

public class EffectContext
{
    private readonly Dictionary<int, float> _results = new();

    public void StoreResult(int id, float value)
    {
        _results[id] = value;
    }

    public float GetResult(int id)
    {
        if (!_results.TryGetValue(id, out var value))
            throw new Exception($"Effect result '{id}' not found");

        return value;
    }
}