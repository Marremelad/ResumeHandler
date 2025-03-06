using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.Models;

public class ContactInformation
{
    [Key]
    public int Id { get; set; }

    [StringLength(254)]
    public required string Email { get; set; }

    [StringLength(12)]
    public required string PhoneNumber { get; set; }
}