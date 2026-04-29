using System.Collections.Generic;

public class MoveExecutedCommandData
{
    public Character Player;
    public Character Enemy;
    public MoveExecutedCommandData(Character player, Character enemy)
    {
        Player = player;
        Enemy = enemy;
    }
}