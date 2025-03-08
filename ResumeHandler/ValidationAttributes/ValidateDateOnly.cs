using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.ValidationAttributes;

public class ValidDateOnly : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateOnly dateOnly)
        {
            if (dateOnly > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return new ValidationResult("The date cannot be in the future.");
            }
        }
        else
        {
            return new ValidationResult("Invalid date format.");
        }

        return ValidationResult.Success;
    }
}