public class CombatantFactory
{
    private readonly IMoveView _moveView;


    public CombatantFactory(IMoveView moveView)
    {
        _moveView = moveView;
        // TODO: Add API when available
        // _api = api;
    }

    public Character CreatePlayer(Character baseCharacter)
    {
        baseCharacter.Role = CombatantRole.Player;
        baseCharacter.Input = new PlayerBattleInput(_moveView);
        return baseCharacter;
    }

    public Character CreateEnemy(Character baseCharacter)
    {
        baseCharacter.Role = CombatantRole.Enemy;
        baseCharacter.Input = new AIBattleInput();
        return baseCharacter;
    }
}