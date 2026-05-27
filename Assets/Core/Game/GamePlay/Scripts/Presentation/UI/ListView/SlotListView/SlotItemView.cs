using UnityEngine;

public abstract class SlotItemView<TSlotData, TContentView, TContentData> :
    MonoBehaviour,
    IListItemView<TSlotData>
    where TSlotData : SlotItemData<TContentData>
    where TContentView : MonoBehaviour, IListItemView<TContentData>
    where TContentData : IIdentifiable
{
    [SerializeField]
    private TContentView _contentPrefab;

    [SerializeField]
    private Transform _contentContainer;

    private TContentView _contentView;

    protected TSlotData Data { get; private set; }

    public virtual void ShowData(TSlotData data)
    {
        Data = data;

        RefreshContent();
    }

    protected virtual void RefreshContent()
    {
        if (Data.Content == null)
        {
            ClearContent();
            return;
        }

        if (_contentView == null)
        {
            _contentView =
                Instantiate(
                    _contentPrefab,
                    _contentContainer);
        }

        _contentView.ShowData(Data.Content);
    }

    protected virtual void ClearContent()
    {
        if (_contentView != null)
        {
            Destroy(_contentView.gameObject);

            _contentView = null;
        }
    }

    public TContentView GetContentView()
    {
        return _contentView;
    }
}
