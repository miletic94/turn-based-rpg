using UnityEngine;

public class BattleMoveView : MonoBehaviour
{
    [Header("Moves")]
    [SerializeField] private MoveView _moveViewPrefab;
    [SerializeField] private Transform _movesContainer;


    public MoveView AddMove()
    {
        return Instantiate(_moveViewPrefab, _movesContainer);
    }

    public void ClearMoves()
    {
        foreach (Transform child in _movesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}