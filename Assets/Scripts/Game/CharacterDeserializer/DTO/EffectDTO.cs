public class EffectDTO
{
    public string Id;
    public string Type;
    public string Target;
    public string Category;

    public ValueDTO Value;

    // optional fields depending on type
    public string Stat;
    public string TickBehavior;
    public int Duration;
    public bool IsSource;
}