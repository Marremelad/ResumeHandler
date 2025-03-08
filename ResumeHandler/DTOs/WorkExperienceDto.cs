using System.ComponentModel.DataAnnotations;
using ResumeHandler.ValidationAttributes;

namespace ResumeHandler.DTOs;

public class WorkExperienceDto
{
    [Required]
    public required string JobTitle { get; set; }

    [Required]
    public required string CompanyName { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    [ValidDateOnly]
    public required DateOnly StartDate { get; set; }

    [Required]
    [ValidDateOnly]
    public DateOnly? EndDate { get; set; }
}