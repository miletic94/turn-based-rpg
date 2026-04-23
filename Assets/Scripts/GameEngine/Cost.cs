public struct Cost
{
    public ResourceType Type { get; }
    public int Amount { get; }

    public static readonly Cost None = new(ResourceType.None, 0);

    public Cost(ResourceType type, int amount)
    {
        Type = type;
        Amount = amount;
    }
}