public class XpDTO
{
    public int Current;
    public int Level;
    public int AddEachPlaythrough;
    public Xp ToXp()
    {
        return new Xp
        (
             Current,
             Level,
             AddEachPlaythrough
        );
    }
}