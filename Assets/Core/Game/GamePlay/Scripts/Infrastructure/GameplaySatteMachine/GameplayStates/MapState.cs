using System.Collections.Generic;
using System.Linq;

public class MapState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameplaySceneContext _context;

    private Character _player;
    private List<Character> _enemies;

    public MapState(
        GameplayStateMachine gameplayStateMachine,
        GameplaySceneContext context)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _context = context;
    }

    public void Enter()
    {
        LoadMapData();

        _context.MapBootstrapper.Initialize(
            _enemies,
            OnEnemySelected
        );
    }

    public void Exit()
    {
        _context.MapBootstrapper.Unload();
    }

    private void LoadMapData()
    {
        var characters = new CharacterDeserializer().Deserialize();

        _player = characters[0];

        _enemies = characters
            .Skip(1)
            .ToList();
    }

    private void OnEnemySelected(Character selectedEnemy)
    {
        _gameplayStateMachine.EnterBattle(
            _player,
            selectedEnemy
        );
    }
}