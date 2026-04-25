using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class EffectConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(IEnumerable<IEffect>).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JArray arr = JArray.Load(reader);
        List<IEffect> effects = new List<IEffect>();

        foreach (var obj in arr)
        {
            string type = obj["type"]?.Value<string>();

            IEffect effect = type switch
            {
                "damage" => obj.ToObject<DamageEffect>(serializer),
                "heal" => obj.ToObject<HealEffect>(serializer),
                "buff" => obj.ToObject<StatModifierEffect>(serializer),
                "debuff" => obj.ToObject<StatModifierEffect>(serializer),
                _ => throw new Exception($"Unknown effect type {type}")
            };

            effects.Add(effect);

        }

        return effects;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        => throw new NotFiniteNumberException();
}