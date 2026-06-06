using System.Collections.Generic;

public class Hero
{
    public string Name { get; private set; }
    public float Health { get; private set; }
    public float Attack { get; private set; }
    public float Defense { get; private set; }
    public float Magic { get; private set; }
    public string SpriteAddress { get; private set; }

    public Xp Xp { get; private set; }
    public int AvailableStatPoints { get; private set; }
    public List<string> EnemiesBeaten { get; private set; }
    public List<Move> AvailableMoves { get; private set; }
    public List<Move> EquippedMoves { get; private set; }
    public Hero(string name, float health, float attack, float defense, float magic, string spriteAddress, Xp xp, int availableStatPoints, List<string> enemiesBeaten, List<Move> availableMoves, List<Move> equippedMoves)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Defense = defense;
        Magic = magic;
        SpriteAddress = spriteAddress;
        Xp = xp;
        AvailableStatPoints = availableStatPoints;
        EnemiesBeaten = enemiesBeaten;
        AvailableMoves = availableMoves;
        EquippedMoves = equippedMoves;
    }
    public void AddBeatenEnemy(string enemyName)
    {
        EnemiesBeaten.Add(enemyName);
    }
    public void SetStats(float attack, float defense, float magic)
    {
        Attack = attack;
        Defense = defense;
        Magic = magic;
    }
    public void SetAvailableMoves(List<Move> availableMoves)
    {
        AvailableMoves = availableMoves;
    }
    public void SetEquippedMoves(List<Move> equippedMoves)
    {
        EquippedMoves = equippedMoves;
    }
    public void AddAvailableStatPoints(int amount)
    {
        AvailableStatPoints += amount;
    }
    public void SetAvaialableStatPoints(int amount)
    {
        AvailableStatPoints = amount;
    }
}