using System;
using UnityEngine;

public class Character
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Defence { get; private set; }
    public int Magic { get; private set; }
    public Move[] Moves { get; private set; }
    public Character(string name, int health, int attack, int defence, int magic, Move[] moves)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Defence = defence;
        Magic = magic;
        Moves = moves;
    }
}

public enum EStat
{
    Health,
    Attack,
    Defence,
    Magic
}

public class MoveEffect
{
    public EActionType ActionType { get; private set; }
    public ETargetType TargetType { get; private set; }
    public EStrangth Value { get; private set; }
    public EStat TargetStat { get; private set; }
    public int Duration { get; private set; }
    public EStat? ScalesOff { get; private set; }

    public MoveEffect(EActionType actionType, ETargetType targetType, EStrangth value, EStat targetStat, int duration = 1, EStat? scalesOff = null)
    {
        ActionType = actionType;
        TargetType = targetType;
        Value = value;
        TargetStat = targetStat;
        Duration = duration;
        ScalesOff = scalesOff;
    }

    public enum EActionType
    {
        Give,
        Take
    }
    public enum ETargetType
    {
        Enemy,
        Self
    }
}

public enum EStrangth
{
    None = 0,
    Light = 10,
    Moderate = 20,
    Heavy = 30
}

public class Move
{
    public string Name { get; private set; }
    public EMoveType Type { get; private set; }
    public EStrangth Value { get; private set; }
    public MoveEffect[] MoveEffects { get; private set; }

    public Move(string name, EMoveType moveType, EStrangth value, MoveEffect[] moveEffects = null)
    {
        Name = name;
        Type = moveType;
        Value = value;
        MoveEffects = moveEffects ?? Array.Empty<MoveEffect>();
    }
}
public enum EMoveType
{
    None,
    Physical,
    Magic
}

public class Game : MonoBehaviour
{
    private void Start()
    {
        // ● Slash: Physical, deals moderate damage. Scales off Attack, reduced by target's Defense.
        var slash = new Move("Slash", EMoveType.Physical, EStrangth.Moderate);
        // ● Shield Up: No damage, raises the knight's Defense for two turns.
        var shieldUp = new Move("Shield Up", EMoveType.None, EStrangth.None, new MoveEffect[1] { new MoveEffect(MoveEffect.EActionType.Give, MoveEffect.ETargetType.Self, EStrangth.Light, EStat.Defence) });
        // ● Battle Cry: No damage, raises the knight's Attack for two turns.
        var battleCry = new Move("Battle Cry", EMoveType.None, EStrangth.None, new MoveEffect[1] { new MoveEffect(MoveEffect.EActionType.Give, MoveEffect.ETargetType.Self, EStrangth.Light, EStat.Attack) });
        // ● Second Wind: Heals the knight for a moderate amount. Scales off Magic.
        var secondWind = new Move("Second Wind", EMoveType.None, EStrangth.None, new MoveEffect[1] { new MoveEffect(MoveEffect.EActionType.Give, MoveEffect.ETargetType.Self, EStrangth.Moderate, EStat.Health, 1, EStat.Magic) });

        // Witch
        // ● Shadow Bolt: Magic, deals heavy damage. Scales off Magic.
        var shadowBolt = new Move("Shadow Bolt", EMoveType.Magic, EStrangth.Heavy);
        // ● Drain Life: Magic, deals light damage and heals the witch for the same amount. Scales off Magic.
        var drainLife = new Move("Drain Life", EMoveType.Magic, EStrangth.Light, new MoveEffect[1] { new MoveEffect(MoveEffect.EActionType.Give, MoveEffect.ETargetType.Self, EStrangth.Light, EStat.Health, 1, EStat.Magic) });
        // ● Curse: Lowers the hero's Attack for two turns.
        var curse = new Move("Curse", EMoveType.None, EStrangth.None, new MoveEffect[1] { new MoveEffect(MoveEffect.EActionType.Take, MoveEffect.ETargetType.Enemy, EStrangth.Light, EStat.Attack, 2) });
        // ● Dark Pact: No damage, raises the witch's Magic for two turns at the cost of some of her own HP.
        var darkPact = new Move("Dark Pact", EMoveType.None, EStrangth.None, new MoveEffect[2] { new MoveEffect(MoveEffect.EActionType.Give, MoveEffect.ETargetType.Self, EStrangth.Light, EStat.Magic), new MoveEffect(MoveEffect.EActionType.Take, MoveEffect.ETargetType.Self, EStrangth.Light, EStat.Health, 2) });

        var knight = new Character("Knight", 100, 10, 10, 10, new Move[4] { slash, shieldUp, battleCry, secondWind });
        var witch = new Character("Withc", 100, 10, 10, 10, new Move[4] { shadowBolt, drainLife, curse, darkPact });

        // Giant Spider
        // ● Bite: Physical, deals moderate damage. Scales off Attack, reduced by Defense.
        // ● Web Throw: Physical, deals light damage and lowers the hero's Defense for two turns. Scales off Attack, reduced by Defense.
        // ● Pounce: Physical, deals heavy damage. Scales off Attack, reduced by Defense.
        // ● Skitter: No damage, raises the spider's Defense for two turns.

        // Dragon
        // ● Flame Breath: Magic, deals heavy damage. Scales off Magic.
        // ● Claw Swipe: Physical, deals moderate damage. Scales off Attack, reduced by Defense.
        // ● Intimidate: No damage, lowers the target's Attack for two turns.
        // ● Dragon Scales: No damage, raises the user's Defense for two turns.

        // Goblin Warrior
        // ● Rusty Blade: Physical, deals moderate damage. Scales off Attack, reduced by Defense.
        // ● Dirty Kick: Physical, deals light damage and lowers the target's Defense for two turns. Scales off Attack, reduced by Defense.
        // ● Frenzy: No damage, raises the user's Attack for two turns.
        // ● Headbutt: Physical, deals heavy damage. Scales off Attack, reduced by Defense.

        // Goblin Mage
        // ● Firebolt: Magic, deals moderate damage. Scales off Magic.
        // ● Arcane Surge: No damage, raises the user's Magic for two turns.
        // ● Mana Drain: Magic, deals light damage and lowers the target's Magic for two turns. Scales off Magic.
        // ● Hex Shield: No damage, raises the user's Defense for two turns
    }
}

// DESCRIPTION: Slash: Physical, deals moderate damage. Scales off Attack, reduced by target's Defense.
// [{ Type(Physical), Take(moderate, Health, Enemy, 1) }]

// DESCRIPTION: Shield Up: No damage, raises the knight's Defense for two turns.
// [{Type(None), Give(moderate, Defense, Self), Duration(2)}]
// DESCRITPION: Battle Cry: No damage, raises the knight's Attack for two turns
// [{Type(None), Give(moderate, Attack, Self), Duration(2)}]

// DESCRIPTION: Drain Life: Magic, deals light damage and heals the witch for the sameamount. Scales off Magic
// [{ Type(Magic), Take(light, Health, Enemy), Give(light, Health, Self) }]
// DESCRIPTION: Curse: Lowers the hero's Attack for two turns
// [{ Type(None), Take(light, Attack, Enemy), Duration(2) }]
// DESCRIPTION: Dark Pact: No damage, raises the witch's Magic for two turns at the cost of some of her own HP.
// [ { Type(None), Give(light, Magic, Self), Take(light, Health, Self), Duration(2) }]

// DESCRIPTION: Web Throw: Physical, deals light damage and lowers the hero's Defense for two turns.
// [ {Type(Physical), Take(light, Health, Enemy)}, {Type(None), Take(light, Defence, Enemy), Duration(2)} ] 
// DESCRIPTION: Skitter: No damage, raises the spider's Defense for two turns.
// [ {Type(None), Give(light, Defence, Self), Duration(2) } ]
// So far:
// * Regular move:
//   - Easy to define MoveType
//   - Deals damage
//   - Scales off Magic or Attack
// * Stats effect
//   - It doesn't fit MoveType scheme
//   - It has number of turns
//   - So far - doesn't deal damage
//   - It isn't explicit does it scale off attack or magic
// * Move with baked in stats effect
//   - Has both Move and stats effect and all of their properties
//   - Obvious example: Web Throw: 
//     -- Physical, deals light damage and lowers the hero's Defense for two turns.
// * 