using ResumeHandler.Data;

namespace ResumeHandler.Common;

public class Helper
{
    public static DateOnly ConvertToDateOnly(string dateString) =>
        DateOnly.TryParse(dateString, out var dateOnly) ? dateOnly : default;
}