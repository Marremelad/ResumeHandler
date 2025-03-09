using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeHandler.Models;

public class WorkExperience
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public required int UserIdFk { get; set; }
    public virtual User? User { get; set; }

    [StringLength(50)]
    public required string JobTitle { get; set; }

    [StringLength(50)]
    public required string CompanyName { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public required DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}