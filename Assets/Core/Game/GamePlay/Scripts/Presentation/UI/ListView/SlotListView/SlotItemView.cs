using UnityEngine;

public abstract class SlotItemView
    <TSlotData, TContentView, TContentData> :
    MonoBehaviour,
    IListItemView<TSlotData>
    where TSlotData : SlotItemData<TContentData>
    where TContentView : MonoBehaviour, IListItemView<TContentData>
    where TContentData : class, IIdentifiable
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

    // -------------------------
    // PUBLIC SLOT API
    // -------------------------

    public void SetContent(TContentData content)
    {
        Data.Content = content;

        RefreshContent();
    }

    public void ClearContent()
    {
        Data.Content = null;

        RefreshContent();
    }

    // -------------------------
    // INTERNAL VISUAL SYNC
    // -------------------------

    protected virtual void RefreshContent()
    {
        if (Data.Content == null)
        {
            DestroyContentView();
            return;
        }

        EnsureContentViewExists();

        _contentView.ShowData(Data.Content);
    }

    protected virtual void EnsureContentViewExists()
    {
        if (_contentView != null)
            return;

        _contentView =
            Instantiate(
                _contentPrefab,
                _contentContainer);
    }

    protected virtual void DestroyContentView()
    {
        if (_contentView == null)
            return;

        Destroy(_contentView.gameObject);

        _contentView = null;
    }

    public TContentView GetContentView()
    {
        return _contentView;
    }
}