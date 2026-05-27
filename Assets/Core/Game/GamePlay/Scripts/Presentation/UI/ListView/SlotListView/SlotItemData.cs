public abstract class SlotItemData<TContent> : IIdentifiable where TContent : IIdentifiable
{
    public int Id { get; }

    public TContent Content { get; }

    public SlotItemData(int id, TContent contentData)
    {
        Id = id;
        Content = contentData;
    }
}
