using System.Collections.Generic;
using Newtonsoft.Json;

public class Move
{
    public int Id { get; }
    public string Name { get; }
    public Cost Cost { get; }
    [JsonConverter(typeof(EffectConverter))]
    public List<IEffect> Effects { get; }

    public Move(int id, string name, Cost cost, List<IEffect> effects)
    {
        Id = id;
        Name = name;
        Cost = cost;
        Effects = effects;
    }
}