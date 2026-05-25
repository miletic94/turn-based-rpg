using System.Collections.Generic;
using Newtonsoft.Json;

public class Move
{
    public int Id { get; }
    public string Name { get; }

    public string IconAddress { get; }

    public Cost Cost { get; }
    public List<IMoveEffect> Effects { get; }

    public Move(int id, string name, string iconAddress, Cost cost, List<IMoveEffect> effects)
    {
        Id = id;
        Name = name;
        IconAddress = iconAddress;
        Cost = cost;
        Effects = effects;
    }
}