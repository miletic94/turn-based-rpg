using System;

public interface IUIFeedbackBus
{
    void Publish<T>(T message) where T : IUIFeedbackMessage;
    void Subscribe<T>(Action<T> handler) where T : IUIFeedbackMessage;
    void Unsubscribe<T>(Action<T> handler) where T : IUIFeedbackMessage;
}