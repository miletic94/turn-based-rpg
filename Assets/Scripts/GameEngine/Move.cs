using System.Collections.Generic;

public class Move
{
    public int Id { get; }
    public string Name { get; }
    public Cost Cost { get; }
    public IReadOnlyList<IEffect> Effects { get; }

    public Move(int id, string name, Cost cost, IReadOnlyList<IEffect> effects)
    {
        Id = id;
        Name = name;
        Cost = cost;
        Effects = effects;
    }
}