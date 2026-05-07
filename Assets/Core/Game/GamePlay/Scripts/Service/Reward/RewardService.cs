using System.Collections.Generic;

public class RewardService
{
    List<Move> _rewardMoves;
    public RewardService(List<Move> rewardMoves)
    {
        _rewardMoves = rewardMoves;
    }
    public List<Move> GetRewards()
    {
        return _rewardMoves;
    }
}