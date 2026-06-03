public struct StatSaveData
{
    public float Attack { get; }
    public float Defense { get; }
    public float Magic { get; }
    public int AvailableStatPoints { get; }

    public StatSaveData(float attack, float defense, float magic, int availableStatPoints)
    {
        Attack = attack;
        Defense = defense;
        Magic = magic;
        AvailableStatPoints = availableStatPoints;
    }
}