using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeHandler.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(35)]
    public required string FirstName { get; set; }

    [StringLength(35)]
    public required string LastName { get; set; }

    [StringLength(1000)]
    public required string Description { get; set; }

    [ForeignKey("ContactInformation")]
    public required int ContactInformationFk { get; set; }
    public virtual ContactInformation? ContactInformation { get; set; }

    public virtual List<Education>? Educations { get; set; }

    public virtual List<WorkExperience>? WorkExperiences { get; set; }
}