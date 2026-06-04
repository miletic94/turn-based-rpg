using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ListView<TView, TData> : MonoBehaviour
    where TView : MonoBehaviour, IListItemView<TData>
    where TData : IIdentifiable
{
    [SerializeField] private TView _prefab;
    [SerializeField] private Transform _container;

    private readonly Dictionary<int, TView> _viewsById = new();

    public List<TView> Render(IReadOnlyList<TData> dataList)
    {
        var activeIds = new HashSet<int>();

        foreach (var data in dataList)
        {
            activeIds.Add(data.Id);

            if (_viewsById.TryGetValue(data.Id, out var existingView))
            {
                existingView.ShowData(data);
            }
            else
            {
                CreateView(data);
            }
        }

        RemoveMissingViews(activeIds);

        return _viewsById.Values.ToList();
    }
    public List<TView> Refresh(IReadOnlyList<TData> dataList)
    {
        foreach (var data in dataList)
        {
            if (_viewsById.TryGetValue(data.Id, out var existingView))
            {
                existingView.ShowData(data);
            }
        }
        return _viewsById.Values.ToList();
    }

    private void CreateView(TData data)
    {
        var view = Instantiate(_prefab, _container);

        view.ShowData(data);

        _viewsById.Add(data.Id, view);
    }

    private void RemoveMissingViews(HashSet<int> activeIds)
    {
        var idsToRemove = new List<int>();

        foreach (var id in _viewsById.Keys)
        {
            if (!activeIds.Contains(id))
            {
                idsToRemove.Add(id);
            }
        }

        foreach (var id in idsToRemove)
        {
            Destroy(_viewsById[id].gameObject);

            _viewsById.Remove(id);
        }
    }
}