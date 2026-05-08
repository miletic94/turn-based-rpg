public class XpDTO
{
    public int Current;
    public int Level;
    public int AddEachPlaythrough;
    public int RewardPoints;
    public Xp ToXp()
    {
        return new Xp
        (
             Current,
             Level,
             AddEachPlaythrough,
             RewardPoints
        );
    }
}