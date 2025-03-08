using Microsoft.EntityFrameworkCore;
using ResumeHandler.Common;
using ResumeHandler.Data;
using ResumeHandler.DTOs;
using ResumeHandler.Models;

namespace ResumeHandler.Services;

public class UserService(ResumeHandlerDbContext context)
{
    public async Task<Response<List<UserDto>?>> GetAllUsersAsync()
    {
        try
        {
            var test = await context.Users
                .Include(u => u.WorkExperiences)
                .Include(u => u.Educations)
                .Select(u => new UserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Description = u.Description,
                    Educations = CreateEducationDto(u.Educations!),
                    WorkExperiences = CreateWorkExperienceDto(u.WorkExperiences!)
                })
                .ToListAsync();

            return Response<List<UserDto>>.Success(test);
        }
        catch (Exception ex)
        {
            return Response<List<UserDto>>.Failure(ex.Message);
        }
    }
    
    public async Task<Response<UserDto?>> GetUserAsync(int id)
    {
        try
        {
            var user = await context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Description = u.Description,
                    Educations = CreateEducationDto(u.Educations!),
                    WorkExperiences = CreateWorkExperienceDto(u.WorkExperiences!)
                })
                .FirstOrDefaultAsync();

            return user == null ? Response<UserDto>.NotFound("User not found") 
                : Response<UserDto>.Success(user);
        }
        catch (Exception ex)
        {
            return Response<UserDto>.Failure(ex.Message);
        }
    }

    private static List<EducationDto> CreateEducationDto(List<Education> educations)
    {
        return educations.Select(e => new EducationDto
        {
            SchoolName = e.SchoolName,
            Degree = e.Degree,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate
        }).ToList();
    }

    private static List<WorkExperienceDto> CreateWorkExperienceDto(List<WorkExperience> workExperiences)
    {
        return workExperiences.Select(w => new WorkExperienceDto
        {
            JobTitle = w.JobTitle,
            CompanyName = w.CompanyName,
            Description = w.Description,
            StartDate = w.StartDate,
            EndDate = w.EndDate
        }).ToList();
    }
}