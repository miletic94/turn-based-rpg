using System.Collections.Generic;

public class MoveDTO
{
    public int Id;
    public string Name;
    public Cost Cost;
    public List<EffectDTO> Effects;
    public Move ToMove() => new Move
    (
        Id,
         Name,
         Cost,
         Effects.ConvertAll(e => e.ToEffect())
    );
}