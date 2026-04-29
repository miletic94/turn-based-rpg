using UnityEngine;
public class CombatantView : MonoBehaviour
{
    private Character _character;
    [SerializeField] private HealthBarView _healthBar;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Bind(Character character)
    {
        _character = character;
    }

    public void FlipSpriteX(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    public void UpdateHealthBar(Character character)
    {
        if (character.Name == _character.Name)
        {
            _healthBar.SetImmediate(character.Health / _character.BaseHealth);
        }
    }
}