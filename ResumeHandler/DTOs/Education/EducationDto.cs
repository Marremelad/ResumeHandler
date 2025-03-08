using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.DTOs.Education;

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