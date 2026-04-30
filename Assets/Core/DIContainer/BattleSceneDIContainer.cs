using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSceneDIContainer : MonoBehaviour, IDiContainer
{
    [SerializeField] MoveView _moveView;
    [SerializeField] CombatantViewFactory _combatantViewFactory;
    [SerializeField] StatView _statView;
    private Dictionary<Type, object> _dependencies;
    private CombatantViewBinder _combatantViewBinder;
    private StatController _statController;
    private MoveController _moveController;
    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = new EventBus();
        _combatantViewBinder = new CombatantViewBinder(_combatantViewFactory, _eventBus);
        _statController = new StatController(_statView);
        _moveController = new MoveController(_moveView);
        _dependencies = new Dictionary<Type, object>
        {
            {typeof(StatController), _statController},
            {typeof(CombatantViewBinder), _combatantViewBinder}
        };
    }
    private async void Start()
    {
        var battleController = new BattleController(
            _eventBus,
            _combatantViewBinder,
            _moveController,
            _statController
        );

        try
        {
            await battleController.Run();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
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