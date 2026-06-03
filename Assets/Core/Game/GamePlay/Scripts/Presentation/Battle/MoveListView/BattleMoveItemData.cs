using UnityEngine;

// TODO: If RewardItemData doesn't add anything to MoveItemData 
// we can use MoveItemData directly in BattleMoveItem
public class BattleMoveItemData : MoveItemData
{
    public BattleMoveItemData(int id, Move move, Sprite sprite) : base(id, move, sprite) { }
}