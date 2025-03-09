using System.ComponentModel.DataAnnotations;
using ResumeHandler.ValidationAttributes;

namespace ResumeHandler.DTOs.WorkExperience;

public class WorkExperienceCreateDto
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [StringLength(50)]
    public required string JobTitle { get; set; }

    [Required]
    [StringLength(50)]
    public required string CompanyName { get; set; }

    [Required]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [ValidStartDate]
    public required string StartDate { get; set; }
    
    public string? EndDate { get; set; }
}