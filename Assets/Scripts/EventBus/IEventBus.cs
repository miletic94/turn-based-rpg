using System;

public interface IEventBus
{
    void Publish<T>(T evt);
    void Subscribe<T>(Action<T> handler);
}