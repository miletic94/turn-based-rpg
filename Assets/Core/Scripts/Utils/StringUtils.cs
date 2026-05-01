public static class StringUtils
{
    public static string ToNoSpaceLowercase(string name)
    {
        return name.Replace(" ", "").ToLowerInvariant();
    }
}