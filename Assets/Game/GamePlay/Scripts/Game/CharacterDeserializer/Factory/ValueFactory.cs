using System;
using System.Collections.Generic;

public static class ValueFactory
{
    private static readonly Dictionary<string, Func<ValueDTO, IValue>> map
        = new();

    public static void Register(string type, Func<ValueDTO, IValue> factory)
    {
        map[type] = factory;
    }

    public static IValue Create(ValueDTO dto)
    {
        if (!map.TryGetValue(dto.Type, out var factory))
            throw new Exception($"Unknown value type {dto.Type}");
        return factory(dto);
    }
}