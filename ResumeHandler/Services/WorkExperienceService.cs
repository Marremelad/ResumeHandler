using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ResumeHandler.Common;
using ResumeHandler.Data;
using ResumeHandler.DTOs.WorkExperience;

namespace ResumeHandler.Services;

public class WorkExperienceService(ResumeHandlerDbContext context)
{
    public async Task<Response<WorkExperienceDto?>> CreateWorkExperience(WorkExperienceCreateDto newWorkExperience)
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

            var startDate = Helper.ConvertToDateOnly(newWorkExperience.StartDate);
            var endDate = Helper.ConvertToDateOnly(newWorkExperience.EndDate!);
            
            if (endDate < startDate)
            {
                return Response<WorkExperienceDto>.ValidationError("End date can not be before start date");
            }

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
}