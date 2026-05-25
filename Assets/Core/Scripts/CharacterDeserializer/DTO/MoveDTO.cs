using System.Collections.Generic;
using Newtonsoft.Json;

public class MoveDTO
{
    public int Id;
    public string Name;
    public string IconAddress;
    public Cost Cost;
    public List<EffectDTO> Effects;
    public Move ToMove()
    {
        return new Move
     (
         Id,
         Name,
         IconAddress,
          Cost,
          Effects.ConvertAll(e => e.ToEffect())
     );
    }
}