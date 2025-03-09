using ResumeHandler.Data;

namespace ResumeHandler.Common;

public class Helper
{
    public static DateOnly? ConvertToDateOnly(string dateString)
    {
        if (string.IsNullOrEmpty(dateString)) return null;
        return DateOnly.TryParse(dateString, out var dateOnly) ? dateOnly : default;
    }
}