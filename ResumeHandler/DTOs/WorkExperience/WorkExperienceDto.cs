using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.DTOs.WorkExperience;

public class WorkExperienceDto
{
    [Required]
    public required string JobTitle { get; set; }

    [Required]
    public required string CompanyName { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public required DateOnly StartDate { get; set; }

    [Required]
    public DateOnly? EndDate { get; set; }
}