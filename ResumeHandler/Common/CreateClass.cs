using ResumeHandler.DTOs;
using ResumeHandler.DTOs.Education;
using ResumeHandler.DTOs.WorkExperience;
using ResumeHandler.Models;

namespace ResumeHandler.Common;

public class CreateClass
{
    public static Education CreateEducation(EducationCreateDto dto, DateOnly startDate, DateOnly endDate)
    {
        return new Education
        {
            UserIdFk = dto.UserId,
            SchoolName = dto.SchoolName,
            Degree = dto.Degree,
            Description = dto.Description,
            StartDate = startDate,
            EndDate = endDate
        };
    }
    
    public static WorkExperience CreateWorkExperience(WorkExperienceCreateDto dto, DateOnly startDate, DateOnly endDate)
    {
        return new WorkExperience
        {
            UserIdFk = dto.UserId,
            CompanyName = dto.CompanyName,
            JobTitle = dto.JobTitle,
            Description = dto.Description,
            StartDate = startDate,
            EndDate = endDate
        };
    }
    
    public static List<EducationDto> CreateEducationDto(List<Education> educations)
    {
        return educations.Select(e => new EducationDto
        {
            Id = e.Id,
            SchoolName = e.SchoolName,
            Degree = e.Degree,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate
        }).ToList();
    }
    
    // Overloaded for single instance of Education object.
    public static EducationDto CreateEducationDto(Education education)
    {
        return new EducationDto
        {
            Id = education.Id,
            SchoolName = education.SchoolName,
            Degree = education.Degree,
            Description = education.Description,
            StartDate = education.StartDate,
            EndDate = education.EndDate
        };
    }

    public static List<WorkExperienceDto> CreateWorkExperienceDto(List<WorkExperience> workExperiences)
    {
        return workExperiences.Select(w => new WorkExperienceDto
        {
            Id = w.Id,
            JobTitle = w.JobTitle,
            CompanyName = w.CompanyName,
            Description = w.Description,
            StartDate = w.StartDate,
            EndDate = w.EndDate
        }).ToList();
    }

    // Overloaded for single instance of WorkExperience object.
    public static WorkExperienceDto CreateWorkExperienceDto(WorkExperience workExperience)
    {
        return new WorkExperienceDto
        {
            Id = workExperience.Id,
            JobTitle = workExperience.JobTitle,
            CompanyName = workExperience.CompanyName,
            Description = workExperience.Description,
            StartDate = workExperience.StartDate,
            EndDate = workExperience.EndDate
        };
    }
}