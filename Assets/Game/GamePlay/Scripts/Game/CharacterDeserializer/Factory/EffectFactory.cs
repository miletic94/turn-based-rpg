using System;
using System.Collections.Generic;

public static class EffectFactory
{
    private static readonly Dictionary<string, Func<EffectDTO, IEffect>> map
        = new();

    public static void Register(string type, Func<EffectDTO, IEffect> factory)
    {
        map[type] = factory;
    }

    public static IEffect Create(EffectDTO dto)
    {
        if (!map.TryGetValue(dto.Type, out var factory))
            throw new Exception($"Unknown effect type {dto.Type}");

        return factory(dto);
    }
}