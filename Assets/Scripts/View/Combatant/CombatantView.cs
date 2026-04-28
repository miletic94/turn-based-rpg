using UnityEngine;
public class CombatantView : MonoBehaviour
{
    private Character _character;
    [SerializeField] private HealthBarView _healthBar;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Bind(Character character, BattleEventBus bus)
    {
        _character = character;

        bus.Subscribe<MoveExecutedEvent>(OnMoveExecuted);
    }

    public void FlipSpriteX(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    private void OnMoveExecuted(MoveExecutedEvent moveExecutedEvent)
    {
        if (moveExecutedEvent.Target == _character)
        {
            if (moveExecutedEvent.Stat == StatType.Health)
            {
                UpdateHealth(moveExecutedEvent.After);
            }
        }
    }

    private void UpdateHealth(float currentHealth)
    {
        _healthBar.SetImmediate(currentHealth / _character.BaseHealth);
    }
}