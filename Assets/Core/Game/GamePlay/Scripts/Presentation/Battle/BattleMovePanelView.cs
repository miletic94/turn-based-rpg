using System.Collections.Generic;
using UnityEngine;

public class BattleMovePanelView : MonoBehaviour
{
    [SerializeField] BattleMoveListView _list;

    public void ShowMoves(List<BattleMoveItemData> moves)
    {
        _list.Render(moves);
    }
    public BattleMoveItem GetView(int id)
    {
        return _list.GetView(id);
    }
}