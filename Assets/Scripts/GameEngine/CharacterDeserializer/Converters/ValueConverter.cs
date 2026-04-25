using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ValueConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(IValue);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        string type = obj["type"]?.Value<string>();

        IValue value = type switch
        {
            "flat" => obj.ToObject<FlatValue>(serializer),
            "scaled" => obj.ToObject<ScaledValue>(serializer),
            "reference" => obj.ToObject<ReferenceValue>(serializer),
            _ => throw new Exception($"Unknown value type {type}")
        };
        return value;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}