using UnityEngine;
public class CombatantView : MonoBehaviour
{
    private Character _character;
    [SerializeField] private HealthBarView _healthBar;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Bind(Character character)
    {
        _character = character;

        UpdatePosition();
        UpdateUI();
    }

    public void FlipSpriteX(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    private void UpdateUI()
    {
        _healthBar.SetImmediate(0.6f);
    }

    private void UpdatePosition()
    {
        // place on battlefield (left/right side etc.)
    }
}