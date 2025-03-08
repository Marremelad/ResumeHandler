using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ResumeHandler.Common;
using ResumeHandler.Data;
using ResumeHandler.DTOs.Education;

namespace ResumeHandler.Services;

public class EducationService(ResumeHandlerDbContext context)
{
    public async Task<Response<EducationDto?>> CreateEducation(EducationCreateDto newEducation)
    {
        try
        {
            var validationContext = new ValidationContext(newEducation);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(newEducation, validationContext, validationResult, true);

            if (!isValid) return Response<EducationDto>.ValidationError(string.Join("; ", validationResult.Select(vr => vr.ErrorMessage)));

            var user = await context.Users
                .Where(u => u.Id == newEducation.UserId)
                .FirstOrDefaultAsync();

            if (user == null) return Response<EducationDto>.NotFound($"User with id {newEducation.UserId} does not exist");
            
            var startDate = Helper.ConvertToDateOnly(newEducation.StartDate);
            var endDate = Helper.ConvertToDateOnly(newEducation.EndDate!);
        
            if (endDate < startDate)
            {
                return Response<EducationDto>.ValidationError("End date can not be before start date");
            }

            var education = CreateClass.CreateEducation(newEducation, startDate, endDate);
            context.Educations.Add(education);
            await context.SaveChangesAsync();
            
            return Response<EducationDto>.Success(CreateClass.CreateEducationDto(education));
        }
        catch (Exception ex)
        {
            return Response<EducationDto>.Failure(ex.Message);
        }
    }
}