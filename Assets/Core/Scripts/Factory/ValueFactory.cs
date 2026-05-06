using System;
using System.Collections.Generic;

public static class ValueFactory
{
    public static IMoveEffectValue Create(ValueDTO dto)
    {
        switch (dto.Type)
        {
            case "flat":
                return new FlatValue(dto.BaseValue);
            case "scaled":
                return new ScaledValue(
                    dto.BaseValue,
                    EnumUtils.ParseEnum<StatType>(dto.ScalesOff),
                    EnumUtils.ParseEnum<StatType>(dto.ReducedBy)
                );
            case "reference":
                return new ReferenceValue(dto.SourceId);
            default:
                throw new Exception($"Unknown value type: {dto.Type}");
        }
    }
}
