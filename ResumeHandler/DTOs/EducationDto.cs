using System.ComponentModel.DataAnnotations;
using ResumeHandler.ValidationAttributes;

namespace ResumeHandler.DTOs;

public class EducationDto
{
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
    [ValidDateOnly]
    public required DateOnly StartDate { get; set; }

    [Required]
    [ValidDateOnly]
    public DateOnly? EndDate { get; set; }
}