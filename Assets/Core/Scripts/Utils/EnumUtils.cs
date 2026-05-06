using System;

public class EnumUtils
{
    public static T ParseEnum<T>(string value)
    {
        if (!Enum.TryParse(typeof(T), value, true, out var result))
            throw new Exception($"Invalid {typeof(T).Name}: {value}");

        return (T)result;
    }
}