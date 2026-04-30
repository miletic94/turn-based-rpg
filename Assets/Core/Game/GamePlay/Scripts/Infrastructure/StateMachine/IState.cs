using UnityEngine;

public interface IState
{
    Awaitable Enter();
    Awaitable Exit();
}