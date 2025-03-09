using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeHandler.Models;

public class Education
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public required int UserIdFk { get; set; }
    public virtual User? User { get; set; }

    [StringLength(50)]
    public required string SchoolName { get; set; }

    [StringLength(50)]
    public string? Degree { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public required DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}