using System;
public class XpService
{
    private readonly Xp _xp;
    public XpService(Xp xp)
    {
        _xp = xp;
    }

    public UpdateXpResult UpdateXp()
    {
        var previousXp = _xp.Current;
        var previousLevel = _xp.Level;
        var xpToNextLevel = CalculateXpForNextLevel();

        _xp.SetCurrent(previousXp + _xp.AddEachPlaythrough);

        if (_xp.Current >= xpToNextLevel)
        {
            _xp.NextLevel();
            _xp.SetCurrent(_xp.Current % xpToNextLevel);
        }

        var result = new UpdateXpResult
        {
            PreviousXp = previousXp,
            CurrentXp = _xp.Current,
            PreviousLevel = previousLevel,
            CurrentLevel = _xp.Level,
            XpToNextLevel = CalculateXpForNextLevel()
        };

        return result;
    }
    public int CalculateXpForNextLevel()
    {
        // TODO: Formula should be defined by designers through data. This is to not think about parser for prototype

        // Formula : 100 * 1.5 ^ (level - 1)
        return (int)(100 * Math.Pow(1.5, (double)_xp.Level - 1));
    }
}