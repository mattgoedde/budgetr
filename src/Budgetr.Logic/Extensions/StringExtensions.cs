namespace Budgetr.Logic.Extensions;

public static class StringExtensions
{
    public static bool IsValidUrl(this string str)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            _ = new Uri(str);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
