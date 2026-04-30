using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class CharacterDeserializer
{
    public List<Character> Deserialize()
    {
        RegisterEffects();
        RegisterValues();

        string charactersPath = Path.Combine(Application.streamingAssetsPath, "Characters.json");
        string charactersJson = File.ReadAllText(charactersPath);

        List<CharacterDTO> characterDTOs = JsonConvert.DeserializeObject<List<CharacterDTO>>(charactersJson);

        List<Character> characters = characterDTOs.Select(dto => CharacterFactory.Create(dto)).ToList();
        return characters;
    }

    private static void RegisterEffects()
    {
        EffectFactory.Register("damage", dto => new DamageEffect(
            dto.Id,
            ParseEnum<TargetType>(dto.Target),
            ParseEnum<EffectCategory>(dto.Category),
            ValueFactory.Create(dto.Value),
            dto.IsSource
        ));

        EffectFactory.Register("heal", dto => new HealEffect(
            dto.Id,
            ParseEnum<TargetType>(dto.Target),
            ValueFactory.Create(dto.Value),
            dto.IsSource
        ));

        EffectFactory.Register("buff", dto => CreateModifier(dto, ModifierType.Buff));
        EffectFactory.Register("debuff", dto => CreateModifier(dto, ModifierType.Debuff));
    }

    private static void RegisterValues()
    {
        ValueFactory.Register("flat", dto => new FlatValue(dto.BaseValue));

        ValueFactory.Register("scaled", dto => new ScaledValue(
            (int)dto.BaseValue,
            ParseEnum<StatType>(dto.ScalesOff),
            ParseEnum<StatType>(dto.ReducedBy)
        ));

        ValueFactory.Register("reference", dto => new ReferenceValue(dto.SourceId));
    }



    private static IMoveEffect CreateModifier(EffectDTO dto, ModifierType type)
    {
        return new StatModifierEffect(
            dto.Id,
            ParseEnum<ModifierType>(dto.Type),
            ParseEnum<TargetType>(dto.Target),
            ParseEnum<StatType>(dto.Stat),
            ValueFactory.Create(dto.Value),
            ParseEnum<ModifierTickBehavior>(dto.TickBehavior),
            dto.Duration,
            dto.IsSource
        );
    }

    private static T ParseEnum<T>(string value)
    {
        if (!Enum.TryParse(typeof(T), value, true, out var result))
            throw new Exception($"Invalid {typeof(T).Name}: {value}");

        return (T)result;
    }
}