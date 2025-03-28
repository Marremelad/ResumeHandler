﻿using System.ComponentModel.DataAnnotations;
using ResumeHandler.ValidationAttributes;

namespace ResumeHandler.DTOs.Education;

public class EducationCreateDto
{
    [Required]
    public required int UserId { get; set; }
    
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

    public string? EndDate { get; set; }
}