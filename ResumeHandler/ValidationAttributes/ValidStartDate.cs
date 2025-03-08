﻿using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.ValidationAttributes;

public class ValidStartDate : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string dateString && DateOnly.TryParse(dateString, out var dateOnly))
        {
            if (dateOnly > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return new ValidationResult("Start date cannot be in the future.");
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid date format for start date. Try YYYY-mm-DD");
    }
}
