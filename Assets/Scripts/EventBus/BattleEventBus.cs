using System;
using System.Collections.Generic;

public class BattleEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Publish<T>(T evt)
    {
        if (_handlers.TryGetValue(typeof(T), out var list))
        {
            foreach (var handler in list)
                ((Action<T>)handler)(evt);
        }
    }

    public void Subscribe<T>(Action<T> handler)
    {
        if (!_handlers.ContainsKey(typeof(T)))
            _handlers[typeof(T)] = new List<Delegate>();

        _handlers[typeof(T)].Add(handler);
    }
}