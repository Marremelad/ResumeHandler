using System.ComponentModel.DataAnnotations;
using ResumeHandler.ValidationAttributes;

namespace ResumeHandler.DTOs;

public class EducationDto
{
    [Required]
    public required string SchoolName { get; set; }

    [Required]
    public string? Degree { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public required DateOnly StartDate { get; set; }

    [Required]
    public DateOnly? EndDate { get; set; }
}