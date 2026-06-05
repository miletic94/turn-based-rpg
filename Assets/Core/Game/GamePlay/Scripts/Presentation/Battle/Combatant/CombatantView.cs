using System.Collections.Generic;
using UnityEngine;
public class CombatantView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private MoveTelegraphView _moveTelegraph;
    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
    public async Awaitable ShowData(List<MoveTelegraphData> dataList)
    {
        await _moveTelegraph.ShowData(dataList);
    }
    public void FlipSpriteX(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}