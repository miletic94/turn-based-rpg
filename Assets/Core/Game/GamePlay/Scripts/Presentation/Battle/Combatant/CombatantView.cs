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
    public async Awaitable ShowData(MoveTelegraphData data)
    {
        await _moveTelegraph.ShowData(data);
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