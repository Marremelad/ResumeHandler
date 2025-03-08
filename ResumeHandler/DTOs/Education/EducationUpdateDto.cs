using System.ComponentModel.DataAnnotations;
using ResumeHandler.ValidationAttributes;

namespace ResumeHandler.DTOs.Education;

public class EducationUpdateDto
{
    [Required]
    public required int EducationId { get; set; }
    
    [Required]
    [StringLength(50)]
    public required string SchoolName { get; set; }

    [Required]
    [StringLength(50)]
    public string? Degree { get; set; }

    [Required]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [ValidStartDate]
    public required string StartDate { get; set; }

    [Required]
    [ValidEndDate]
    public string? EndDate { get; set; }
}