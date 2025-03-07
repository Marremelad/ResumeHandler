using System.ComponentModel.DataAnnotations;

namespace ResumeHandler.DTOs;

public class UserDto
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required string Description { get; set; }
}