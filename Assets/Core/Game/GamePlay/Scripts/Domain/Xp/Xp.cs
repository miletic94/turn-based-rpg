public class Xp
{
    public int Current { get; private set; }
    public int Level { get; private set; }
    public int AddEachPlaythrough { get; private set; }
    public Xp(int current, int level, int addEachPlaythrough)
    {
        Current = current;
        Level = level;
        AddEachPlaythrough = addEachPlaythrough;
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