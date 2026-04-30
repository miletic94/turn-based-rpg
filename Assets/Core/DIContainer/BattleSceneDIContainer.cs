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
    private StatViewBinder _statViewBinder;
    private MoveViewBinder _moveViewBinder;
    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = new EventBus();
        _combatantViewBinder = new CombatantViewBinder(_combatantViewFactory, _eventBus);
        _statViewBinder = new StatViewBinder(_eventBus, _statView);
        _moveViewBinder = new MoveViewBinder(_eventBus, _moveView);
        _dependencies = new Dictionary<Type, object>
        {
            {typeof(StatViewBinder), _statViewBinder},
            {typeof(CombatantViewBinder), _combatantViewBinder}
        };
    }
    private async void Start()
    {
        var battleController = new BattleController(
            _eventBus,
            _combatantViewBinder,
            _moveViewBinder,
            _statViewBinder
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