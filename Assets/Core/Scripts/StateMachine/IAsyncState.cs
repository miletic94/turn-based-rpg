using UnityEngine;

public interface IAsyncState
{
    Awaitable Enter();
    Awaitable Exit();
}