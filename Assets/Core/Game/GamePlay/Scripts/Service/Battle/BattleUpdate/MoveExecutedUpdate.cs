using System.Collections.Generic;

public sealed class MoveExecutedUpdate : BattleUpdate
{
    public List<IEffectResult> MoveResult { get; }
    public MoveExecutedUpdate(List<IEffectResult> moveResult)
    {
        MoveResult = moveResult;
    }
}