using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ResumeHandler.Common;
using ResumeHandler.Data;
using ResumeHandler.DTOs.Education;

namespace ResumeHandler.Services;

public class EducationService(ResumeHandlerDbContext context)
{
    public async Task<Response<List<EducationDto>?>> GetAllEducations()
    {
        try
        {
            var educations = await context.Educations
                .ToListAsync();

            return Response<List<EducationDto>>.Success(CreateClass.CreateEducationDto(educations));
        }
        catch (Exception ex)
        {
            return Response<List<EducationDto>>.Failure(ex.Message);
        }
    }

    public async Task<Response<EducationDto?>> GetEducation(int id)
    {
        try
        {
            var education = await context.Educations
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            return education == null ? Response<EducationDto>.NotFound("Education not found") 
                : Response<EducationDto>.Success(CreateClass.CreateEducationDto(education));
        }
        catch (Exception ex)
        {
            return Response<EducationDto>.Failure(ex.Message);
        }
    }
    
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

    public async Task<Response<EducationDto?>> UpdateEducation(EducationUpdateDto updatedEducation)
    {
        try
        {
            var validationContext = new ValidationContext(updatedEducation);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(updatedEducation, validationContext, validationResult, true);

            if (!isValid) return Response<EducationDto>.ValidationError(string.Join("; ", validationResult.Select(vr => vr.ErrorMessage)));

            var education = await context.Educations
                .Where(e => e.Id == updatedEducation.Id)
                .FirstOrDefaultAsync();

            if (education == null) return Response<EducationDto>.NotFound($"Education with id {updatedEducation.Id} does not exist");
            
            var startDate = Helper.ConvertToDateOnly(updatedEducation.StartDate);
            var endDate = Helper.ConvertToDateOnly(updatedEducation.EndDate!);
        
            if (endDate < startDate)
            {
                return Response<EducationDto>.ValidationError("End date can not be before start date");
            }

            if (updatedEducation.SchoolName != "string") education.SchoolName = updatedEducation.SchoolName;
            if (updatedEducation.Degree != "string") education.Degree = updatedEducation.Degree;
            if (updatedEducation.Description != "string") education.Description = updatedEducation.Description;
            education.StartDate = startDate;
            education.EndDate = endDate;

            await context.SaveChangesAsync();

            return Response<EducationDto>.Success(CreateClass.CreateEducationDto(education));
        }
        catch (Exception ex)
        {
            return Response<EducationDto>.Failure(ex.Message);
        }
    }
}