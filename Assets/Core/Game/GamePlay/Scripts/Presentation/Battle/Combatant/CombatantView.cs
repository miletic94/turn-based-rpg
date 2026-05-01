using UnityEngine;
public class CombatantView : MonoBehaviour
{
    [SerializeField] private HealthBarView _healthBar;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void FlipSpriteX(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    public void UpdateHealthBar(Combatant character)
    {
        _healthBar.SetImmediate(character.Health / character.BaseHealth);
    }
    public void Dispose()
    {
        Destroy(gameObject);
    }
}