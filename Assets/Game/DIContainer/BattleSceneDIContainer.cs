using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSceneDIContainer : MonoBehaviour, IDiContainer
{
    [SerializeField] MoveView _moveView;
    [SerializeField] CombatantViewFactory _combatantViewFactory;
    [SerializeField] StatView _statView;
    private Dictionary<Type, MonoBehaviour> _monoBehaviourDependencies;
    private CombatantController _combatantController;
    private Dictionary<Type, object> _dependencies;
    private ICommandFactory _commandFactory;

    private void Awake()
    {
        _monoBehaviourDependencies = new Dictionary<Type, MonoBehaviour>()
        {
            {typeof(MoveView), _moveView},
            {typeof(CombatantFactory),_combatantViewFactory},
            {typeof(StatView), _statView}
        };

        _commandFactory = new CommandFactory(this);
        _combatantController = new CombatantController();
        _dependencies = new Dictionary<Type, object>()
        {
            {typeof(CombatantController), _combatantController}
        };
    }
    private async void Start()
    {
        var combatantFactory = new CombatantFactory(_moveView);

        var characters = new CharacterDeserializer().Deserialize();

        var knight = characters[0];

        combatantFactory.CreatePlayer(knight);
        _combatantController.Register(knight, _combatantViewFactory.CreateView(knight));

        _statView.ShowStat(knight);

        var witch = characters[1];
        combatantFactory.CreateEnemy(witch);
        _combatantController.Register(witch, _combatantViewFactory.CreateView(witch));


        BattleState battleState = new BattleState(new List<Character>() { knight, witch });
        MoveExecutor moveExecutor = new MoveExecutor();
        BattleController battleController = new BattleController(_commandFactory, battleState, moveExecutor);

        _moveView.SetMoves(knight.Moves);

        try
        {
            await battleController.BattleLoop();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public T ResolveMonoBehaviourDependency<T>() where T : MonoBehaviour
    {
        if (!_monoBehaviourDependencies.TryGetValue(typeof(T), out var dependency))
        {
            throw new Exception($"Dependency of Type {typeof(T)} not found");
        }
        return (T)dependency;
    }
    public T Resolve<T>()
    {
        if (!_dependencies.TryGetValue(typeof(T), out var dependency))
        {
            throw new Exception($"Dependency of Type {typeof(T)} not found");
        }
        return (T)dependency;
    }
}