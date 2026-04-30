using UnityEngine;

public static class AwaitableUtils
{
    static readonly AwaitableCompletionSource completionSource = new();

    public static Awaitable CompletedTask
    {
        get
        {
            completionSource.SetResult();
            var awaitable = completionSource.Awaitable;
            completionSource.Reset();
            return awaitable;
        }
    }
}