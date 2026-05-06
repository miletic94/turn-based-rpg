using System;
public static class EffectFactory
{

    public static IMoveEffect Create(EffectDTO dto)
    {
        switch (dto.Type)
        {
            case "damage":
                return new DamageEffect(
                            dto.Id,
                            EnumUtils.ParseEnum<TargetType>(dto.Target),
                            EnumUtils.ParseEnum<EffectCategory>(dto.Category),
                            dto.Value.ToValue(),
                            dto.IsSource
                        );
            case "heal":
                return new HealEffect(
                dto.Id,
                EnumUtils.ParseEnum<TargetType>(dto.Target),
                dto.Value.ToValue(),
                dto.IsSource
                );
            case "buff":
                return CreateModifier(dto, ModifierType.Buff);
            case "debuff":
                return CreateModifier(dto, ModifierType.Debuff);
            default:
                throw new Exception($"Unknown effect type: {dto.Type}");
        }
    }
    private static IMoveEffect CreateModifier(EffectDTO dto, ModifierType type)
    {
        return new StatModifierEffect(
            dto.Id,
            EnumUtils.ParseEnum<ModifierType>(dto.Type),
            EnumUtils.ParseEnum<TargetType>(dto.Target),
            EnumUtils.ParseEnum<StatType>(dto.Stat),
            dto.Value.ToValue(),
            EnumUtils.ParseEnum<ModifierTickBehavior>(dto.TickBehavior),
            dto.Duration,
            dto.IsSource
        );
    }
}