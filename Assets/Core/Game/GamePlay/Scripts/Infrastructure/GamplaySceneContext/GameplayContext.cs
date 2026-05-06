using System.Collections.Generic;

public class GameplayContext
{
    public Hero Hero { get; private set; }
    public List<Character> Enemies { get; private set; }
    public Character CurrentEnemy;

    public GameplayContext(Hero hero, List<Character> enemies)
    {
        Hero = hero;
        Enemies = enemies;
    }
}