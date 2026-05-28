public class MoveManagementMoveItem : MoveListItem, IDragDataSource
{
    public object GetDragData()
    {
        return new MoveDragData { MoveId = _data.Id };
    }
}