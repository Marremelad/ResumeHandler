namespace ResumeHandler.Common;

public class Helper
{
    public static DateOnly ConvertToStartDateOnly(string dateString)
    {
        return DateOnly.Parse(dateString);
    }
    
    public static DateOnly? ConvertToEndDateOnly(string dateString)
    {
        if (string.IsNullOrEmpty(dateString)) return null;
        return DateOnly.TryParse(dateString, out var dateOnly) ? dateOnly : default;
    }
}