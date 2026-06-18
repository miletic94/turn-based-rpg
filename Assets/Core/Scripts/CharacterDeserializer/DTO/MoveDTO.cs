using System.Collections.Generic;
using System.Linq;

public class MoveDTO
{
    public int Id;
    public string Name;
    public string IconAddress;
    public Cost Cost;
    public List<HealthModifierDTO> HealthModifiers;
    public List<StatModifierDTO> StatModifiers;
    public Move ToMove()
    {
        return new Move
     (
        Id,
        Name,
        IconAddress,
        Cost,
        HealthModifiers.Select(modifier => modifier.ToHealthModifier()).ToList(),
        StatModifiers.Select(modifier => modifier.ToStatModifier()).ToList()
     );
    }
}