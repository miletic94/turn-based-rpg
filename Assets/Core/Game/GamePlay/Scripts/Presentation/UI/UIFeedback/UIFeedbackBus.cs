using System;
using System.Collections.Generic;

public class UIFeedbackBus : IUIFeedbackBus
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new();
    // TODO: Can we make this type safe
    public void Publish<T>(T message) where T : IUIFeedbackMessage
    {
        if (_subscribers.TryGetValue(typeof(T), out var list))
        {
            foreach (var del in list)
            {
                ((Action<T>)del)?.Invoke(message);
            }
        }
    }

    public void Subscribe<T>(Action<T> handler) where T : IUIFeedbackMessage
    {
        if (!_subscribers.TryGetValue(typeof(T), out var list))
        {
            list = new List<Delegate>();
            _subscribers[typeof(T)] = list;
        }
        list.Add(handler);
    }

    public void Unsubscribe<T>(Action<T> handler) where T : IUIFeedbackMessage
    {
        if (_subscribers.TryGetValue(typeof(T), out var list))
        {
            list.Remove(handler);
        }
    }
}