using ResumeHandler.DTOs.Education;
using ResumeHandler.DTOs.WorkExperience;

namespace ResumeHandler.DTOs;

public class UserDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Description { get; set; }

    public List<EducationDto>? Educations { get; set; }
    
    public List<WorkExperienceDto>? WorkExperiences { get; set; }
}