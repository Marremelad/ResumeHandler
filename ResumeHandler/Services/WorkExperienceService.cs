using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ResumeHandler.Common;
using ResumeHandler.Data;
using ResumeHandler.DTOs.WorkExperience;

namespace ResumeHandler.Services;

public class WorkExperienceService(ResumeHandlerDbContext context)
{
    public async Task<Response<List<WorkExperienceDto>?>> GetAllWorkExperiences()
    {
        try
        {
            var workExperiences = await context.WorkExperiences
                .ToListAsync();

            return Response<List<WorkExperienceDto>>.Success(CreateClass.CreateWorkExperienceDto(workExperiences));
        }
        catch (Exception ex)
        {
            return Response<List<WorkExperienceDto>>.Failure(ex.Message);
        }
    }

    public async Task<Response<WorkExperienceDto?>> GetWorkExperience(int id)
    {
        try
        {
            var workExperience = await context.WorkExperiences
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (workExperience == null) return Response<WorkExperienceDto>.NotFound("Work experience not found");

            return Response<WorkExperienceDto>.Success(CreateClass.CreateWorkExperienceDto(workExperience));
        }
        catch (Exception ex)
        {
            return Response<WorkExperienceDto>.Failure(ex.Message);
        }
    }
    
    public async Task<Response<WorkExperienceDto?>> AddWorkExperience(WorkExperienceCreateDto newWorkExperience)
    {
        try
        {
            var validationContext = new ValidationContext(newWorkExperience);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(newWorkExperience, validationContext, validationResult, true);

            if (!isValid) return Response<WorkExperienceDto>.ValidationError(string.Join("; ", validationResult.Select(vr => vr.ErrorMessage)));

            var user = await context.Users
                .Where(u => u.Id == newWorkExperience.UserId)
                .FirstOrDefaultAsync();

            if (user == null) return Response<WorkExperienceDto>.NotFound($"User with id {newWorkExperience.UserId} does not exist");

            var startDate = Helper.ConvertToStartDateOnly(newWorkExperience.StartDate);
            var endDate = Helper.ConvertToEndDateOnly(newWorkExperience.EndDate!);

            if (endDate == default(DateOnly)) return Response<WorkExperienceDto>.ValidationError("Invalid date format for end date. Try YYYY-mm-DD");
            
            if (endDate < startDate) return Response<WorkExperienceDto>.ValidationError("End date can not be before start date");
            
            var workExperience = CreateClass.CreateWorkExperience(newWorkExperience, startDate, endDate);

            context.WorkExperiences.Add(workExperience);
            await context.SaveChangesAsync();

            return Response<WorkExperienceDto>.Success(CreateClass.CreateWorkExperienceDto(workExperience));
        }
        catch (Exception ex)
        {
            return Response<WorkExperienceDto>.Failure(ex.Message);
        }
    }
    
    public async Task<Response<WorkExperienceDto?>> UpdateWorkExperience(WorkExperienceUpdateDto updatedWorkExperience)
    {
        try
        {
            var validationContext = new ValidationContext(updatedWorkExperience);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(updatedWorkExperience, validationContext, validationResult, true);

            if (!isValid) return Response<WorkExperienceDto>.ValidationError(string.Join("; ", validationResult.Select(vr => vr.ErrorMessage)));

            var workExperience = await context.WorkExperiences
                .Where(w => w.Id == updatedWorkExperience.Id)
                .FirstOrDefaultAsync();

            if (workExperience == null) return Response<WorkExperienceDto>.NotFound($"Work experience with id {updatedWorkExperience.Id} does not exist");

            var startDate = Helper.ConvertToStartDateOnly(updatedWorkExperience.StartDate);
            var endDate = Helper.ConvertToEndDateOnly(updatedWorkExperience.EndDate!);

            if (endDate == default(DateOnly)) return Response<WorkExperienceDto>.ValidationError("Invalid date format for end date. Try YYYY-mm-DD");
            
            if (endDate < startDate) return Response<WorkExperienceDto>.ValidationError("End date can not be before start date");
            
            if (updatedWorkExperience.JobTitle != "string") workExperience.JobTitle = updatedWorkExperience.JobTitle;
            if (updatedWorkExperience.CompanyName != "string") workExperience.CompanyName = updatedWorkExperience.CompanyName;
            if (updatedWorkExperience.Description != "string") workExperience.Description = updatedWorkExperience.Description;
            workExperience.StartDate = startDate;
            workExperience.EndDate = endDate;

            await context.SaveChangesAsync();

            return Response<WorkExperienceDto>.Success(CreateClass.CreateWorkExperienceDto(workExperience));
        }
        catch (Exception ex)
        {
            return Response<WorkExperienceDto>.Failure(ex.Message);
        }
    }
    
    public async Task<Response<WorkExperienceDto?>> RemoveWorkExperience(int id)
    {
        try
        {
            var workExperience = await context.WorkExperiences
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (workExperience == null) return Response<WorkExperienceDto>.NotFound("Work experience not found");

            context.WorkExperiences.Remove(workExperience);
            await context.SaveChangesAsync();

            return Response<WorkExperienceDto>.Success(CreateClass.CreateWorkExperienceDto(workExperience));
        }
        catch (Exception ex)
        {
            return Response<WorkExperienceDto>.Failure(ex.Message);
        }
    }
}