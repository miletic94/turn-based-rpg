using UnityEngine;

public interface IDiContainer
{
    T ResolveMonoBehaviourDependency<T>() where T : MonoBehaviour;
    T Resolve<T>();
}