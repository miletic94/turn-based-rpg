public interface IBattleCombatant
{
    CombatantRole Role { get; set; }
    IBattleInput Input { get; set; }
}