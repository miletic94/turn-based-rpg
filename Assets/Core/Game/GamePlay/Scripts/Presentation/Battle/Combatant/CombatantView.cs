using UnityEngine;
public class CombatantView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
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