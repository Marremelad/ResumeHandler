using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(35)]
    public required string FirstName { get; set; }

    [StringLength(35)]
    public required string LastName { get; set; }
    
    [StringLength(254)]
    public required string Email { get; set; }

    [StringLength(12)]
    public required string PhoneNumber { get; set; }

    [StringLength(1000)]
    public required string Description { get; set; }
    
    public virtual List<Education>? Educations { get; set; }

    public virtual List<WorkExperience>? WorkExperiences { get; set; }
}