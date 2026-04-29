using UnityEngine;

public interface IDiContainer
{
    T Resolve<T>();
}