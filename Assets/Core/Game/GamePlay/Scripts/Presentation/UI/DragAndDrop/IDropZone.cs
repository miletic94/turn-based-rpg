public interface IDropZone
{
    bool CanAccept(DragContext context);
    void Accept(DragContext context);
}