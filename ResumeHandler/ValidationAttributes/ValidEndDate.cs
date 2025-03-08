using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.ValidationAttributes;

public class ValidEndDate : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string dateString && DateOnly.TryParse(dateString, out var dateOnly))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid date format for end date. Try YYYY-mm-DD");
    }
}