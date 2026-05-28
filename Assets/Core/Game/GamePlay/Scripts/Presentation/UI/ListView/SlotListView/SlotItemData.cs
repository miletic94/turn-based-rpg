public abstract class SlotItemData<TContent> : IIdentifiable where TContent : IIdentifiable
{
    public int Id { get; }

    public TContent Content;

    public SlotItemData(int id, TContent contentData)
    {
        Id = id;
        Content = contentData;
    }
}
