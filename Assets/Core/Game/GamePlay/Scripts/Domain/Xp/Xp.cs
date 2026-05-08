public class Xp
{
    public int Current { get; private set; }
    public int Level { get; private set; }
    public int AddEachPlaythrough { get; private set; }
    public int RewardPoints { get; private set; }
    public Xp(int current, int level, int addEachPlaythrough, int rewardPoints)
    {
        Current = current;
        Level = level;
        AddEachPlaythrough = addEachPlaythrough;
        RewardPoints = rewardPoints;
    }
    public void SetCurrent(int value)
    {
        Current = value;
    }
    public void NextLevel()
    {
        Level++;
    }
}