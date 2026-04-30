using System;

public interface IEventBus
{
    void Publish<TEvent>(TEvent evt)
        where TEvent : IGameEvent;

    void Subscribe<TEvent>(Action<TEvent> handler)
        where TEvent : IGameEvent;

    void Unsubscribe<TEvent>(Action<TEvent> handler)
        where TEvent : IGameEvent;
}