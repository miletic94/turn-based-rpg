using UnityEngine;
using UnityEngine.UI;


public class MoveListItem :
    MonoBehaviour,
    IListItemView<MoveItemData>
{
    [SerializeField]
    private Image _icon;

    protected MoveItemData _data;

    public void ShowData(MoveItemData data)
    {
        _data = data;

        _icon.sprite = data.IconSprite;
    }

    public MoveItemData GetData()
    {
        return _data;
    }
}