using System.Collections.Generic;
public class MapState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;
    private List<Character> _enemies;

    public MapState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context,
        Hero hero)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }
    public MapState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context
    )
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public void Enter()
    {
        LoadMapData();

        _context.MapBootstrapper.Initialize(
            _enemies,
            OnEnemySelected,
            OnManageMovesButtonClicked
        );
    }

    public void Exit()
    {
        _context.MapBootstrapper.Unload();
    }

    private void LoadMapData()
    {
        var deserializer = new CharacterDeserializer();
        _enemies = deserializer.DeserializeCharacter().ConvertAll(dto => dto.ToCharacter());
        // TODO. Move this to where we enter GameplayState
        _context.InitializeRun(deserializer.DeserializeHero().ToHero());
    }

    private void OnManageMovesButtonClicked()
    {
        _gameplayStateMachine.EnterMoveManagement();
    }

    private void OnEnemySelected(Character selectedEnemy)
    {
        _gameplayStateMachine.EnterBattle(
            _context.RunSession.Hero.ToCharacter(),
            selectedEnemy
        );
    }
}