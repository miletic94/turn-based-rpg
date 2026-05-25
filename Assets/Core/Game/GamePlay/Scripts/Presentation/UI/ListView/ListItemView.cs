using UnityEngine;

public abstract class ListItemView<TData> : MonoBehaviour
{
    public abstract void ShowData(TData data);
}